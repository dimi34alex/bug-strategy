using TMPro;
using UnityEngine;

namespace BugStrategy.UI.Elements.FloatStorageViews
{
    public class FloatStorageTextView : FloatStorageView
    {
        [SerializeField] private TMP_Text text;

        protected override void UpdateView() 
            => text.text = $"{Storage.CurrentValueInt}/{Storage.Capacity}";
    }
}