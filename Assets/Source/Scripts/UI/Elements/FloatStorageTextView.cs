using TMPro;
using UnityEngine;

namespace BugStrategy.UI.Elements
{
    public class FloatStorageTextView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private IReadOnlyFloatStorage _storage;

        public void SetStorage(IReadOnlyFloatStorage storage)
        {
            if(storage == _storage)
                return;

            if (_storage != null) 
                _storage.Changed -= UpdateView;

            _storage = storage;
            _storage.Changed += UpdateView;
        }

        private void UpdateView() 
            => text.text = $"{_storage.CurrentValueInt}/{_storage.Capacity}";
    }
}