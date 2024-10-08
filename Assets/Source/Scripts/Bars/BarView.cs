﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.Bars
{
    public class BarView : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private Image _dynamicBar;

        private float _updateValueDuration = 0.2f;
        private float _updateValueDynamicRatio =4;

        private IReadOnlyFloatStorage _storage;

        public void Init(IReadOnlyFloatStorage storage)
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
            if (_storage.Capacity == 0)
                return;
            float storageRatio = _storage.CurrentValue / _storage.Capacity;
            _bar.transform.DOScaleX(_storage.CurrentValue / _storage.Capacity, _updateValueDuration);
            if (_dynamicBar!=null)
                _dynamicBar.transform.DOScaleX(_storage.CurrentValue / _storage.Capacity, _updateValueDuration* _updateValueDynamicRatio);
        }
    }
}
