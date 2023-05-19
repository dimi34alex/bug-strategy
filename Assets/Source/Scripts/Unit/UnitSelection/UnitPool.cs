using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPool : MonoBehaviour
{
    public List<GameObject> units;
    public List<GameObject> group1;
    public List<GameObject> group2;
    public List<GameObject> group3;
    public List<GameObject> group4;
    public List<GameObject> group5;

    public static UnitPool Instance { get; private set; }
    
    void Awake()
    {
        if (Instance != null){
            Destroy(this);
            return;
        }

        Instance = this;
    }
    
    void Update()
    {
        if (Input.GetButton("left shift"))
        {
            if (Input.GetButton("1"))
            {
                CreateGroup(group1);
            }
            if (Input.GetButton("2"))
            {
                CreateGroup(group2);
            }
            if (Input.GetButton("3"))
            {
                CreateGroup(group3);
            }
            if (Input.GetButton("4"))
            {
                CreateGroup(group4);
            }
            if (Input.GetButton("5"))
            {
                CreateGroup(group5);
            }
        }
        else
        {
            if (Input.GetButton("1"))
            {
                SelectGroup(group1);
            }
            if (Input.GetButton("2"))
            {
                SelectGroup(group2);
            }
            if (Input.GetButton("3"))
            {
                SelectGroup(group3);
            }
            if (Input.GetButton("4"))
            {
                SelectGroup(group4);
            }
            if (Input.GetButton("5"))
            {
                SelectGroup(group5);
            }
        }

    }
    public void UnitCreation(GameObject unit)
    {
        units.Add(unit);
    }

    public void SelectionCheck()
    {
        foreach (GameObject unit in units)
        {
            Transform selection;
            selection = unit.gameObject.transform.GetChild(1);

            if (selection.transform.gameObject.activeSelf && !unit.GetComponent<MovingUnit>().isSelected)
            {
                selection.transform.gameObject.SetActive(false);
            }

            if (!selection.transform.gameObject.activeSelf && unit.GetComponent<MovingUnit>().isSelected)
            {
                selection.transform.gameObject.SetActive(true);
            }
        }
    }

    void CreateGroup(List<GameObject> group)
    {
        ClearGroup(group);

        foreach (GameObject unit in units)
        {
            if (unit.GetComponent<MovingUnit>().isSelected == true)
            {
                group.Add(unit);
            }
        }
    }

    public void SelectGroup(List<GameObject> group)
    {
        UnitSelection.Instance.DeselectAll();
        
        foreach (GameObject groupUnit in group)
        {
            groupUnit.GetComponent<MovingUnit>().isSelected = true;
        }

        SelectionCheck();
    }

    public void SelectGroupButton()
    {
        SelectGroup(group1);
    }

    void ClearGroup(List<GameObject> group)
    {
        group.Clear();
    }
}
