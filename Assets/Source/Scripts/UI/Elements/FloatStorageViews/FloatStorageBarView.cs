using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.UI.Elements.FloatStorageViews
{
    public class FloatStorageBarView : FloatStorageView
    {
        [SerializeField] private Image _bar;
        [SerializeField] private Image _dynamicBar;

        private const float UpdateValueDuration = 0.2f;
        private const float UpdateValueDynamicRatio = 4;

        protected override void UpdateView()
        {
            if (Storage.Capacity == 0)
                return;
            
            _bar.transform.DOScaleX(Storage.FillPercentage, UpdateValueDuration);
            if (_dynamicBar!=null)
                _dynamicBar.transform.DOScaleX(Storage.FillPercentage, UpdateValueDuration* UpdateValueDynamicRatio);
        }
    }
}
