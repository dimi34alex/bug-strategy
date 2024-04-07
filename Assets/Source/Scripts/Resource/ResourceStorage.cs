using System;
using UnityEngine;

public interface IReadOnlyResourceStorage
{
    public float CurrentValue { get; }
    public int CurrentValueInt { get; }
    public float Capacity { get; }

    public event Action Changed;
    public event Action ResourceChanged;
    public event Action<float> ResourceAdded;
    public event Action<float> ResourceRemoved;
    public event Action CapacityChanged;
}

[Serializable]
public class ResourceStorage : IReadOnlyResourceStorage
{
    [SerializeField] private float _currentValue;
    [SerializeField] private float _capacity;

    public float CurrentValue => _currentValue;
    public int CurrentValueInt => (int)CurrentValue;
    public float Capacity => _capacity;

    public event Action Changed;
    public event Action ResourceChanged;
    public event Action<float> ResourceAdded;
    public event Action<float> ResourceRemoved;
    public event Action CapacityChanged;

    public ResourceStorage(float currentValue, float capacity)
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
            ResourceAdded?.Invoke(_currentValue - oldValue);
        else
            ResourceRemoved?.Invoke(oldValue - _currentValue);

        ResourceChanged?.Invoke();
        Changed?.Invoke();
    }

    public void SetValue(float value)
    {
        _currentValue = Mathf.Clamp(value, 0f, _capacity);
        ResourceChanged?.Invoke();
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

