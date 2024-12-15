using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUnit : MonoBehaviour
{
    [SerializeField] private Button UpgradeUnitBtn;
    private bool activeFunc = false;

    private void Awake ()
    {
        UpgradeUnitBtn.onClick.AddListener(OnUpgradeUnitButtonClicked);
    }

    private void OnUpgradeUnitButtonClicked ()
    {
        Image image = UpgradeUnitBtn.GetComponent<Image>();

        if(image.color == Color.black)
        {
            image.color = Color.white;
            activeFunc = false;
        }
        else
        {
            image.color = Color.black;
            activeFunc = true;
        }
    }

    public bool GetActiveFunc ()
    {
        return activeFunc;
    }
}
