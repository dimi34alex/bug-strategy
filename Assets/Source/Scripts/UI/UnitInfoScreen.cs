using UnityEngine;
using UnityEngine.UI;

public class UnitInfoScreen : EntityInfoScreen
{
}

public class EntityInfoScreen : UIScreen
{
    [SerializeField] private Image _infoImage;
    private HealthView _healthView;

    private IReadOnlyResourceStorage _health;

    private void Awake()
    {
        OnAwake();
    }

    public void OnAwake()
    {
        _healthView = gameObject.GetComponentInChildren<HealthView>(true);
    }

    public void SetInfo(Sprite sprite, IReadOnlyResourceStorage storage)
    {
        _healthView.Init(storage);

        _health = storage;

        _infoImage.sprite = sprite;
    }
}