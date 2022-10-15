using UnityEngine;
using UnityEngine.AI;

public abstract class AttackUnit : MovingUnit
{
    public sealed override UnitType UnitType => UnitType.AttackUnit;
}
