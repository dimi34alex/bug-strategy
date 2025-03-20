using System;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public class ConstructionSelector
    {
        private readonly IConstructionGrid _constructionGrid;

        public ConstructionBase LastSelectedConstruction { get; private set; }
        public ConstructionBase SelectedConstruction { get; private set; }

        public event Action OnSelectionChange;

        public ConstructionSelector(IConstructionGrid constructionGrid)
        {
            _constructionGrid = constructionGrid;
        }

        public bool TrySelect(Ray ray, AffiliationEnum playerAffiliation)
        {
            if (Physics.Raycast(ray, out var hitInfo, 50f, CustomLayerID.Construction_Ground.Cast<int>()))
            {
                var position = _constructionGrid.RoundPositionToGrid(hitInfo.point);

                if (_constructionGrid.ConstructionExist(position))
                {
                    var playerConstructionSelected = false;
                    var construction = _constructionGrid.GetConstruction(position);
                    if (construction.Affiliation == playerAffiliation)
                    {
                        construction.Select(true);
                        if (SelectedConstruction != null)
                            SelectedConstruction.Deselect();
                        LastSelectedConstruction = SelectedConstruction;
                        SelectedConstruction = construction;
                        playerConstructionSelected = true;
                    }
                    else
                    {
                        construction.Select(false);
                        if (SelectedConstruction != null)
                            SelectedConstruction.Deselect();
                        LastSelectedConstruction = SelectedConstruction;
                        SelectedConstruction = construction;
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                        playerConstructionSelected = true;
#endif
                    }

                    OnSelectionChange?.Invoke();
                    return playerConstructionSelected;
                }
            }

            Deselect();
            
            return false;
        }

        public void Deselect()
        {
            if (SelectedConstruction != null)
                SelectedConstruction.Deselect();
    
            LastSelectedConstruction = SelectedConstruction;
            SelectedConstruction = null;
            OnSelectionChange?.Invoke();
        }
    }
}
