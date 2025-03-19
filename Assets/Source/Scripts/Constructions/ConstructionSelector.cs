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
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 50f, CustomLayerID.Construction_Ground.Cast<int>()))
            {
                Vector3 position = _constructionGrid.RoundPositionToGrid(hitInfo.point);

                if (_constructionGrid.ConstructionExist(position))
                {
                    var construction = _constructionGrid.GetConstruction(position);
#if !UNITY_EDITOR
                    if (construction.Affiliation != playerAffiliation)
                    {
                        ResetSelection();
                        return false;
                    }
#endif

                    LastSelectedConstruction = SelectedConstruction;
                    SelectedConstruction = _constructionGrid.GetConstruction(position);
                    OnSelectionChange?.Invoke();

                    return true;
                }
            }

            LastSelectedConstruction = SelectedConstruction;
            SelectedConstruction = null;
            OnSelectionChange?.Invoke();

            return false;
        }

        public void ResetSelection()
        {
            LastSelectedConstruction = SelectedConstruction;
            SelectedConstruction = null;
            OnSelectionChange?.Invoke();
        }
    }
}
