using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBlock : MonoBehaviour
{
    private AbilityBase _abilityBase;
    [SerializeField] private Image Fill;
    [SerializeField] private Image Icon;

    public void SetIcon(Sprite icon, AbilityBase abilityBase)
    {
        _abilityBase = abilityBase;
        Icon.sprite = icon;
    }

    private void Update()
    {
        Fill.fillAmount = 1 - _abilityBase.CurrentTime/_abilityBase.ReloadTime;
    }
}
