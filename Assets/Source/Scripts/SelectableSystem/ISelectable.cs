using System;

namespace BugStrategy.SelectableSystem
{
    public interface ISelectable
    {
        public bool IsSelected { get; }
        public event Action<bool> OnSelect;
        public event Action OnDeselect;
        public void Select(bool isFullView);
        public void Deselect();
    }
}