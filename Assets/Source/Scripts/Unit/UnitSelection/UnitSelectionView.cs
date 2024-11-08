using BugStrategy.Selection;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.UnitSelection
{
    public class UnitSelectionView : MonoBehaviour
    {
        [SerializeField] private Texture _texture;

        [Inject] private readonly Selector _selector;
        
        private Vector2 StartSelectionPoint => _selector.StartSelectPoint;
        private Vector2 CurrentSelectionPoint => _selector.CurrentSelectPoint;

        private readonly SelectionGUI _selectionGUI = new();

        /*need cus if we will take _selector.SelectProcess can be situation when we doesnt calculate gui params,
         but _selector.SelectProcess is true, so we will draw selection field with legacy data*/
        private bool _isSelectionProcess;

        private void Update()
        {
            _isSelectionProcess = _selector.IsSelectProcess;
            if (_isSelectionProcess) 
                CalculateSelectionParameters();
        }

        private void CalculateSelectionParameters()
        {
            _selectionGUI.StartPoint = new Vector2(CurrentSelectionPoint.x, Screen.height - CurrentSelectionPoint.y);
            _selectionGUI.Size = new Vector2(StartSelectionPoint.x - CurrentSelectionPoint.x,
                CurrentSelectionPoint.y - StartSelectionPoint.y);
        }

        private void OnGUI()
        {
            if (_isSelectionProcess)
            {
                GUI.DrawTexture(new Rect(_selectionGUI.StartPoint.x, _selectionGUI.StartPoint.y,
                    _selectionGUI.Size.x, _selectionGUI.Size.y), _texture);
            }
        }

        private class SelectionGUI
        {
            public Vector2 StartPoint;
            public Vector2 Size;
        }
    }
}
