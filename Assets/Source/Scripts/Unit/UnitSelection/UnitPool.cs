using System.Collections.Generic;
using UnityEngine;

public class UnitPool : MonoBehaviour
{
    public List<MovingUnit> movingUnits;

    public static UnitPool Instance { get; private set; }

    private Dictionary<GroupID, List<MovingUnit>> _groupsWithID;

    private void Awake()
    {
        _groupsWithID = new Dictionary<GroupID, List<MovingUnit>>();
        _groupsWithID.Add(GroupID.Group1, new List<MovingUnit>());
        _groupsWithID.Add(GroupID.Group2, new List<MovingUnit>());
        _groupsWithID.Add(GroupID.Group3, new List<MovingUnit>());
        _groupsWithID.Add(GroupID.Group4, new List<MovingUnit>());
        _groupsWithID.Add(GroupID.Group5, new List<MovingUnit>());

        if (Instance != null){
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetButton("left shift"))
        {
            if (Input.GetButton("1"))
                CreateGroup(GroupID.Group1);
          
            if (Input.GetButton("2"))
                CreateGroup(GroupID.Group2);

            if (Input.GetButton("3"))
                CreateGroup(GroupID.Group3);

            if (Input.GetButton("4"))
                CreateGroup(GroupID.Group4);

            if (Input.GetButton("5"))
                CreateGroup(GroupID.Group5);
        }
        else
        {
            if (Input.GetButton("1"))
                SelectGroup(GroupID.Group1);
            
            if (Input.GetButton("2"))
                SelectGroup(GroupID.Group2);

            if (Input.GetButton("3"))
                SelectGroup(GroupID.Group3);

            if (Input.GetButton("4"))
                SelectGroup(GroupID.Group4);

            if (Input.GetButton("5"))
                SelectGroup(GroupID.Group5);
        }
    }
    public void UnitCreation(MovingUnit unit)
    {
        movingUnits.Add(unit);
    }

    private void CreateGroup(GroupID id)
    {
        List<MovingUnit> group = _groupsWithID[id];

        ClearGroup(group);

        foreach (MovingUnit unit in movingUnits)
            if (unit.GetComponent<MovingUnit>().IsSelected)
                group.Add(unit);
    }

    public void SelectGroup(GroupID id)
    {
        List<MovingUnit> group = _groupsWithID[id];

        UnitSelection.Instance.DeselectAll();
        
        foreach (MovingUnit groupUnit in group)
            groupUnit.GetComponent<MovingUnit>().Select();
        
    }

    public void SelectGroupButton()
    {
        SelectGroup(GroupID.Group1);
    }

    private void ClearGroup(List<MovingUnit> group)
    {
        group.Clear();
    }
}

public enum GroupID
{
    Group1,
    Group2,
    Group3,
    Group4,
    Group5,
}