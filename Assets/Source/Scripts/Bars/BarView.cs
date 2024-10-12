using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.Bars
{
    public class BarView : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private Image _dynamicBar;

        private const float UpdateValueDuration = 0.2f;
        private const float UpdateValueDynamicRatio = 4;

        private IReadOnlyFloatStorage _storage;

        public void SetStorage(IReadOnlyFloatStorage storage)
        {
            if (storage == _storage)
                return;

            if (_storage != null) 
                _storage.Changed -= UpdateBar;
            
            _storage = storage;
            _storage.Changed += UpdateBar;
            
            UpdateBar();
        }
        
        public virtual void UpdateBar()
        {
            if (_storage.Capacity == 0)
                return;
            
            _bar.transform.DOScaleX(_storage.FillPercentage, UpdateValueDuration);
            if (_dynamicBar!=null)
                _dynamicBar.transform.DOScaleX(_storage.FillPercentage, UpdateValueDuration* UpdateValueDynamicRatio);
        }
    }
}
