using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MovingUnit : UnitBase
{
    public override MiniMapID MiniMapId => MiniMapID.PlayerUnit;

    [SerializeField] private NavMeshAgent _navMeshAgent;

    public override UnitType UnitType => UnitType.MovingUnit;
    public Vector3 Velocity => _navMeshAgent.velocity;

    public bool select;
    public bool isSelected
    {
        get => select;
        set { select = value; }
    }

    [SerializeField] private SomeTestAbility_1 _ability1;
    [SerializeField] private SomeTestAbility_2 _ability2;

    private GameObject WorkerDutyComp;


    void Start()
    {
        UnitPool.Instance.UnitCreation(gameObject);

        if (gameObject.CompareTag("Worker"))
        {
            WorkerDutyComp = gameObject.transform.GetChild(4).gameObject;
        }
    }

    private void Awake()
    {
        _abilites.Add(_ability1);
        _abilites.Add(_ability2);

        _stateMachine = new EntityStateMachine(new[] { new UnitMoveState() }, EntityStateID.Move);
    }

    public void SetDestination(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    public void Warp(Vector3 position)
    {
        _navMeshAgent.Warp(position);
    }

    void Update()
    {
        foreach (var abilite in _abilites)
        {
            abilite.OnUpdate(Time.deltaTime);
        }
    }

    public void GiveOrder(GameObject target, Vector3 position)
    {
        if (!isSelected) return;
        string target_tag = target.tag;

        switch (target_tag)
        {
            case "PollenSource":
                if (gameObject.CompareTag("Worker"))
                {
                    WorkerDutyComp.GetComponent<WorkerDuty>().isFindingRes = true;
                    WorkerDutyComp.GetComponent<WorkerDuty>().WorkingOnGO = target;
                }
                break;

            default:
                if (gameObject.CompareTag("Worker"))
                {
                    WorkerDutyComp.GetComponent<WorkerDuty>().isFindingRes = false;
                    WorkerDutyComp.GetComponent<WorkerDuty>().isGathering = false;
                    WorkerDutyComp.GetComponent<WorkerDuty>().isBuilding = false;
                    WorkerDutyComp.GetComponent<WorkerDuty>().isFindingBuild = false;
                }
                break;
        }
        
        SetDestination(position);
    }

    public void UseFirstAbility()
    {
        _abilites[0].OnUse();
    }   
    public void UseSecondAbility()
    {
        _abilites[1].OnUse();
    }
}
