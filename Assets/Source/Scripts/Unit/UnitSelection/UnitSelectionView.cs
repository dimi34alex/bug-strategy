using BugStrategy.CustomInput;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.UnitSelection
{
    public class UnitSelectionView : MonoBehaviour
    {
        [SerializeField] private Texture _texture;

        [Inject] private readonly IInputProvider _inputProvider;
        
        private bool _isSelecting;

        private Vector2 _mouseStartSelectionPoint;
        private Vector2 _mouseEndSelectionPoint;

        private SelectionGUI _selectionGUI;

        private void Awake()
        {
            _selectionGUI = new SelectionGUI();
        }

        private void Update()
        {
            if (_inputProvider.LmbDown && !_inputProvider.MouseCursorOverUi())
            {
                _isSelecting = true;
                _mouseStartSelectionPoint = _inputProvider.MousePosition;
            }

            CalculateSelectionParamenters();

            if (_inputProvider.LmbUp)
            {
                _isSelecting = false;
                _mouseEndSelectionPoint = _inputProvider.MousePosition;
            }
        }

        private void CalculateSelectionParamenters()
        {
            _selectionGUI.StartPoint = new Vector2(_inputProvider.MousePosition.x,
                Screen.height - _inputProvider.MousePosition.y);
            _selectionGUI.Size = new Vector2(_mouseStartSelectionPoint.x - _inputProvider.MousePosition.x,
                _inputProvider.MousePosition.y - _mouseStartSelectionPoint.y);
        }


        private void OnGUI()
        {
            if (_isSelecting)
            {
                GUI.DrawTexture(new Rect(_selectionGUI.StartPoint.x, _selectionGUI.StartPoint.y,
                    _selectionGUI.Size.x, _selectionGUI.Size.y), _texture);
            }
        }


        public class SelectionGUI
        {
            public Vector2 StartPoint;
            public Vector2 Size;
        }
    }
}
