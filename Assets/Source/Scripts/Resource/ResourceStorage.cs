using System;
using UnityEngine;

[Serializable]
public class ResourceStorage
{
    [SerializeField] private float _currentValue;
    [SerializeField] private float _capacity;

    public float CurrentValue => _currentValue;
    public float Capacity => _capacity;

    public ResourceStorage(float currentValue, float capacity)
    {
        _currentValue = currentValue;
        _capacity = capacity;
    }

    public void ChangeValue(float value)
    {
        _currentValue = Mathf.Clamp(_currentValue + value, 0f, _capacity);
    }

    public void SetValue(float value)
    {
        _currentValue = Mathf.Clamp(value, 0f, _capacity);
    }

    public void SetCapacity(float value)
    {
        _capacity = value;
    }
}
