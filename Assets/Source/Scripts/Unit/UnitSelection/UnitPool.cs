using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPool : MonoBehaviour
{
    public List<MovingUnit> movingUnits;
    public List<MovingUnit> group1;
    public List<MovingUnit> group2;
    public List<MovingUnit> group3;
    public List<MovingUnit> group4;
    public List<MovingUnit> group5;

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
    public void UnitCreation(MovingUnit unit)
    {
        movingUnits.Add(unit);
    }
    
    void CreateGroup(List<MovingUnit> group)
    {
        ClearGroup(group);

        foreach (MovingUnit unit in movingUnits)
        {
            if (unit.GetComponent<MovingUnit>().IsSelected)
            {
                group.Add(unit);
            }
        }
    }

    public void SelectGroup(List<MovingUnit> group)
    {
        UnitSelection.Instance.DeselectAll();
        
        foreach (MovingUnit groupUnit in group)
        {
            groupUnit.GetComponent<MovingUnit>().Select();
        }
    }

    public void SelectGroupButton()
    {
        SelectGroup(group1);
    }

    void ClearGroup(List<MovingUnit> group)
    {
        group.Clear();
    }
}
