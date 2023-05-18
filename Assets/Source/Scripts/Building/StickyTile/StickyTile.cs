using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class StickyTile : ConstructionBase
{
    [SerializeField] private SerializableDictionary<ResourceID, int> _costValues;
    [SerializeField] private TriggerBehaviour _triggerBehaviour;
    public override ConstructionID ConstructionID => ConstructionID.Sticky_Tile_Construction;
    private Cost _cost;
    public override Cost Cost => _cost;

    protected override void OnStart()
    {
        _triggerBehaviour.EnterEvent += OnUnitEnter;
        _triggerBehaviour.ExitEvent += OnUnitExit;
    }

    private void OnUnitEnter(ITriggerable triggerable)
    {
        if (triggerable.TryCast(out MovingUnit movingUnit))
        {
            movingUnit.ChangeContainsStickyTiles(1);
        }
    }

    private void OnUnitExit(ITriggerable triggerable)
    {
        if (triggerable.TryCast(out MovingUnit movingUnit))
        {
            movingUnit.ChangeContainsStickyTiles(-1);
        }
    }

    public override void CalculateCost()
    {
        _cost = new Cost(_costValues);
    }
}
