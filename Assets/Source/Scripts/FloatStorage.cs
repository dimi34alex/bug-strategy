using System;
using UnityEngine;

namespace Source.Scripts
{
    public interface IReadOnlyFloatStorage
    {
        public float CurrentValue { get; }
        public int CurrentValueInt { get; }
        public float Capacity { get; }

        public event Action Changed;
        public event Action ValueChanged;
        public event Action<float> ValueAdded;
        public event Action<float> ValueRemoved;
        public event Action CapacityChanged;
    }

    [Serializable]
    public class FloatStorage : IReadOnlyFloatStorage
    {
        [SerializeField] private float _currentValue;
        [SerializeField] private float _capacity;

        public float CurrentValue => _currentValue;
        public int CurrentValueInt => (int)CurrentValue;
        public float Capacity => _capacity;

        public event Action Changed;
        public event Action ValueChanged;
        public event Action<float> ValueAdded;
        public event Action<float> ValueRemoved;
        public event Action CapacityChanged;

        public FloatStorage(float currentValue, float capacity)
        {
            _currentValue = currentValue;
            _capacity = capacity;
        }

        public void ChangeValue(float value)
        {
            float oldValue = _currentValue;
            _currentValue = Mathf.Clamp(_currentValue + value, 0f, _capacity);

            if (oldValue == _currentValue)
                return;

            if (oldValue < _currentValue)
                ValueAdded?.Invoke(_currentValue - oldValue);
            else
                ValueRemoved?.Invoke(oldValue - _currentValue);

            ValueChanged?.Invoke();
            Changed?.Invoke();
        }

        public void SetValue(float value)
        {
            _currentValue = Mathf.Clamp(value, 0f, _capacity);
            ValueChanged?.Invoke();
            Changed?.Invoke();
        }

        public void SetCapacity(float value)
        {
            _capacity = value;
            CapacityChanged?.Invoke();

            ChangeValue(0f);
            Changed?.Invoke();
        }
    }
}