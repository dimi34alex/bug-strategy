using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SomeTestAbility_1 : AbilityBase
{
    public override void OnInitialization()
    {
        base.OnInitialization();
    }

    public override void OnUpdate(float time)
    {
        base.OnUpdate(time);
        Print();
    }

    public override void OnUse()
    {
        base.OnUse();
    }

    void Print()
    {
        Debug.Log("Print");
    }
}
