using BugStrategy.Unit.AbilitiesCore;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.SelectableSystem
{
    public class AbilityBlock : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private Image icon;

        private IAbility _abilityBase;

        public void SetData(Sprite iconSprite, IAbility abilityBase)
        {
            _abilityBase = abilityBase;
            icon.sprite = iconSprite;

            _abilityBase.Cooldown.OnTick += UpdateFill;
            UpdateFill();
        }

        private void UpdateFill()
        {
            fill.fillAmount = 1 - _abilityBase.Cooldown.FillPercentage;
        }
    
        public void Subscribe()
        {
            _abilityBase.Cooldown.OnTick += UpdateFill;
            UpdateFill();
        }
    
        public void UnSubscribe()
        {
            _abilityBase.Cooldown.OnTick -= UpdateFill;
        }
    
        private void OnDestroy()
        {
            UnSubscribe();
        }
    }
}