using UnityEngine;
using System;
using UnityEngine.UI;

public class ConstructionInfoScreen : EntityInfoScreen
{
    [SerializeField] private Button _upgradeButton;

    public event Action UpgradeClicked;

    private void Awake()
    {
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
    }

    private void OnUpgradeButtonClicked()
    {
        UpgradeClicked?.Invoke();
    }

}
