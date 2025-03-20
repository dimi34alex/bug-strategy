using UnityEngine;

namespace BugStrategy.SelectableSystem
{
    public abstract class SelectableObjectUIBase<TSelectable> : MonoBehaviour
        where TSelectable : ISelectable
    {
        [SerializeField] protected TSelectable selectable;
        [SerializeField] private GameObject canvas;
    
        private void Start() => Initialize();

        protected virtual void Initialize()
        {
            canvas.SetActive(selectable.IsSelected);
        
            selectable.OnSelect += OnSelect;
            selectable.OnDeselect += OnDeselect;
        }

        protected virtual void OnSelect(bool isFullView)
            => canvas.SetActive(true);

        protected virtual void OnDeselect() 
            => canvas.SetActive(false);

        private void OnDestroy() => OnDeselect();
    }
}