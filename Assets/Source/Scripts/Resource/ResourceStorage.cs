using System;
using UnityEngine;

public interface IReadOnlyResourceStorage
{
    public float CurrentValue { get; }
    public int CurrentValueInt { get; }
    public float Capacity { get; }

    public event Action OnChange;
    public event Action OnResourceChange;
    public event Action<float> OnResourceAdd;
    public event Action<float> OnResourceRemove;
    public event Action OnCapacityChange;
}

[Serializable]
public class ResourceStorage : IReadOnlyResourceStorage
{
    [SerializeField] private float _currentValue;
    [SerializeField] private float _capacity;

    public float CurrentValue => _currentValue;
    public int CurrentValueInt => (int)CurrentValue;
    public float Capacity => _capacity;

    public event Action OnChange;
    public event Action OnResourceChange;
    public event Action<float> OnResourceAdd;
    public event Action<float> OnResourceRemove;
    public event Action OnCapacityChange;

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
            OnResourceAdd?.Invoke(_currentValue - oldValue);
        else
            OnResourceRemove?.Invoke(oldValue - _currentValue);

        OnResourceChange?.Invoke();
        OnChange?.Invoke();
    }

    public void SetValue(float value)
    {
        _currentValue = Mathf.Clamp(value, 0f, _capacity);
        OnResourceChange?.Invoke();
        OnChange?.Invoke();
    }

    public void SetCapacity(float value)
    {
        _capacity = value;
        OnCapacityChange?.Invoke();

        ChangeValue(0f);
        OnChange?.Invoke();
    }
}

