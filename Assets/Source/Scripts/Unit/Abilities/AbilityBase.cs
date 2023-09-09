using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AbilityBase
{
    [SerializeField] protected ResourceStorage reloadTimer;
    [SerializeField] protected Sprite abilityIcon;

    public IReadOnlyResourceStorage ReloadTimer => reloadTimer;
    public float ReloadTime => reloadTimer.Capacity;
    public float CurrentTime => reloadTimer.CurrentValue;
    
    public Sprite AbilityIcon => abilityIcon;
    public bool Useable => CurrentTime >= ReloadTime;
    
    public virtual void OnInitialization()
    {
        
    }

    public virtual void OnUpdate(float time)
    {
        reloadTimer.ChangeValue(time);
    }

    public virtual void OnUse()
    {
        if (Useable)
            reloadTimer.SetValue(0);
    }
}
