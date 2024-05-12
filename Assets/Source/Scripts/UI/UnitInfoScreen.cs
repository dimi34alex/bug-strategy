using UnityEngine;
using UnityEngine.UI;

public class UnitInfoScreen : UIScreen
{
    [SerializeField] private Image _infoImage;

    private IReadOnlyResourceStorage _health;

    public void SetInfoUnit(Sprite sprite, IReadOnlyResourceStorage storage)
    {
        if (_health != null)
            _health.Changed -= OnHealthChanged;

        _health = storage;

        _health.Changed += OnHealthChanged;

        _infoImage.sprite = sprite;
    }

    private void OnHealthChanged()
    {

    }
}