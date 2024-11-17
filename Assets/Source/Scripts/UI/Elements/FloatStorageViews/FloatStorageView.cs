using UnityEngine;

namespace BugStrategy.UI.Elements.FloatStorageViews
{
    public abstract class FloatStorageView : MonoBehaviour
    {
        protected IReadOnlyFloatStorage Storage;

        public void SetStorage(IReadOnlyFloatStorage storage)
        {
            if (storage == Storage)
                return;

            if (Storage != null) 
                Storage.Changed -= UpdateView;

            Storage = storage;
            if (Storage == null)
                return;
            
            Storage.Changed += UpdateView;
            
            UpdateView();
        }

        protected abstract void UpdateView();
    }
}