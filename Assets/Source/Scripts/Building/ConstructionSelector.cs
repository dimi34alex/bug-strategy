using System;
using UnityEngine;

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

    public bool TrySelect(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, CustomLayerID.Construction_Ground.Cast<int>()))
        {
            Vector3Int position = _constructionGrid.RoundPositionToGrid(hitInfo.point).ToInt();

            if (_constructionGrid.ConstructionExist(position))
            {
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
}
