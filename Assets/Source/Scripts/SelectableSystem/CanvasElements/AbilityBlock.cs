using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class AbilityBlock : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Image icon;

    private AbilityBase _abilityBase;

    public void SetData(Sprite iconSprite, AbilityBase abilityBase)
    {
        _abilityBase = abilityBase;
        icon.sprite = iconSprite;

        _abilityBase.ReloadTimer.OnChange += UpdateFill;
        UpdateFill();
    }

    private void UpdateFill()
    {
        fill.fillAmount = 1 - _abilityBase.CurrentTime / _abilityBase.ReloadTime;
    }
    
    public void Subscribe()
    {
        _abilityBase.ReloadTimer.OnChange += UpdateFill;
        UpdateFill();
    }
    
    public void UnSubscribe()
    {
        _abilityBase.ReloadTimer.OnChange -= UpdateFill;
    }
    
    private void OnDestroy()
    {
        UnSubscribe();
    }
}