using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BarView : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Image _dynamicBar;

    private float _updateValueDuration = 0.2f;
    private float _updateValueDynamicRatio =4;

    private IReadOnlyResourceStorage _storage;

    public void Init(IReadOnlyResourceStorage storage)
    {
        if (storage == _storage)
            return;

        if (_storage != null)
        {
            _storage.Changed -= UpdateBar;
        }
        _storage = storage;
        _storage.Changed += UpdateBar;
        UpdateBar();
    }

    public virtual void UpdateBar()
    {
        float storageRatio = _storage.CurrentValue / _storage.Capacity;
        _bar.transform.DOScaleX(_storage.CurrentValue / _storage.Capacity, _updateValueDuration);
        _dynamicBar.transform.DOScaleX(_storage.CurrentValue / _storage.Capacity, _updateValueDuration* _updateValueDynamicRatio);
    }
}
