using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.SelectableSystem
{
    public interface OnSelectionUI
    {
        void OnSelect();
        void OnDeselect();
    }

    public class  HealthBar : MonoBehaviour, OnSelectionUI
    {
        [SerializeField] private Slider healthPointsSlider;
        private IReadOnlyFloatStorage _healthStorage;

        private bool _coroutineIsActive;
        private float _targetValue;
        
        public void Init(IReadOnlyFloatStorage healthStorage)
        {
            _healthStorage = healthStorage;
            SetHealthBarValue();
        }

        public void OnSelect()
        {
            _healthStorage.Changed += UpdateHealthBarValue;
            SetHealthBarValue();
        }

        public void OnDeselect()
        {
            _healthStorage.Changed -= UpdateHealthBarValue;
            if (_coroutineIsActive)
            {
                StopCoroutine(ChangeHealthBarValue());
                _coroutineIsActive = false;
            }
        }
        
        private void SetHealthBarValue()
        {
            healthPointsSlider.value = _healthStorage.CurrentValue / _healthStorage.Capacity;
        }
        
        private void UpdateHealthBarValue()
        {
            _targetValue = _healthStorage.CurrentValue / _healthStorage.Capacity;
            if (!_coroutineIsActive)
                StartCoroutine(ChangeHealthBarValue());
        }

        private IEnumerator ChangeHealthBarValue()
        {
            _coroutineIsActive = true;

            while (Mathf.Abs(healthPointsSlider.value - _targetValue) > 0.01f)
            {
                healthPointsSlider.value = Mathf.Lerp(healthPointsSlider.value,_targetValue, 2.5f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            healthPointsSlider.value = _targetValue;
            _coroutineIsActive = false;
        }
    }
}