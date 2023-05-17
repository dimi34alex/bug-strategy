using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MovingUnit : UnitBase
{
    private StateBehaviorUnit _stateBehaviorUnit;
    public StateBehaviorUnit StateBehaviorUnit => _stateBehaviorUnit;

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
    
    void Start()
    {
        UnitPool.Instance.UnitCreation(this);
    }

    private void Awake()
    {
        _stateBehaviorUnit = new StateBehaviorUnit();

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

    public void SetStateBehaviorUnit(StateBehaviorUnitID id)
    {
        _stateBehaviorUnit.SetID(id);
    }

    void Update()
    {
        foreach (var abilite in _abilites)
        {
            abilite.OnUpdate(Time.deltaTime);
        }
    }

    public void GiveOrder(string tag, Vector3 position)
    {
        if(!isSelected) return;
        
        switch (tag)
        {
            case "PollenSource":
                if(gameObject.CompareTag("Worker"))
                    GetComponent<WorkerDuty>().isFindingRes = true;
                break;

            default:
                if (gameObject.CompareTag("Worker"))
                {
                    GetComponent<WorkerDuty>().isFindingRes = false;
                    GetComponent<WorkerDuty>().isGathering = false;
                    GetComponent<WorkerDuty>().isBuilding = false;
                    GetComponent<WorkerDuty>().isFindingBuild = false;
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
