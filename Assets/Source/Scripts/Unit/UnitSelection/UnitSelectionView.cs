using UnityEngine;

public class UnitSelectionView : MonoBehaviour
{
    [SerializeField] private Texture _texture;

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
        if (Input.GetMouseButtonDown(0))
        {
            _isSelecting = true;
            _mouseStartSelectionPoint = Input.mousePosition;
        }

        CalculateSelectionParamenters();

        if (Input.GetMouseButtonUp(0))
        {
            _isSelecting = false;
            _mouseEndSelectionPoint = Input.mousePosition;

        }
    }

    private void CalculateSelectionParamenters()
    {
        _selectionGUI.StartPoint = new Vector2(Input.mousePosition.x,
            Screen.height - Input.mousePosition.y);
        _selectionGUI.Size = new Vector2(_mouseStartSelectionPoint.x - Input.mousePosition.x,
            Input.mousePosition.y - _mouseStartSelectionPoint.y);
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
