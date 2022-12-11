using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AbilityBase
{
    [SerializeField] protected ResourceStorage reloadTimer;
    public float ReloadTime => reloadTimer.Capacity;
    public float CurrentTime => reloadTimer.CurrentValue;
    
    public Sprite AbilityIcon => abilityIcon;
    [SerializeField] protected Sprite abilityIcon;
    public bool Useble => CurrentTime >= ReloadTime;
    
    public virtual void OnInitialization()
    {
        
    }

    public virtual void OnUpdate(float time)
    {
        reloadTimer.ChangeValue(time);
    }

    public virtual void OnUse()
    {
        if (Useble)
            reloadTimer.SetValue(0);
    }
}
