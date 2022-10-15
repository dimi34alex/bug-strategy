using System;
using UnityEngine;

public class AttackUnitTriggerBehaviour : TriggerZone
{
    [SerializeField] private SphereCollider _sphereCollider;

    protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => component => component is AttackUnit;

    public void SetTriggerRadius(float value)
    {
        _sphereCollider.radius = value;
    }
}
