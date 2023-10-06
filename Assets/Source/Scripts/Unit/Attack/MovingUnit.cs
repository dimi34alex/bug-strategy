using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MovingUnit : UnitBase
{
    private StateBehaviorUnit _stateBehaviorUnit;
    public StateBehaviorUnit StateBehaviorUnit => _stateBehaviorUnit;
    
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private float _startMaxSpeed;

    public override UnitType UnitType => UnitType.MovingUnit;
    public Vector3 Velocity => _navMeshAgent.velocity;
    
    [SerializeField] private SomeTestAbility_1 _ability1;
    [SerializeField] private SomeTestAbility_2 _ability2;

    private WorkerDuty _workerDuty;


    void Start()
    {
        UnitPool.Instance.UnitCreation(this);

        if (gameObject.CompareTag("Worker"))
        {
            _workerDuty = GetComponentInChildren<WorkerDuty>();
        }
    }

    private void Awake()
    {
        _stateBehaviorUnit = new StateBehaviorUnit();

        _abilites.Add(_ability1);
        _abilites.Add(_ability2);

        _stateMachine = new EntityStateMachine(new[] { new UnitMoveState() }, EntityStateID.Move);
        _startMaxSpeed = _navMeshAgent.speed;
    }

    public void SetDestination(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    public void Warp(Vector3 position)
    {
        _navMeshAgent.Warp(position);
    }

    private int _containsStickyTilesCount;

    public void ChangeContainsStickyTiles(int delta)
    {
        _containsStickyTilesCount += delta;
        Debug.Log(_containsStickyTilesCount);

        if (_containsStickyTilesCount is 0)
            _navMeshAgent.speed = _startMaxSpeed;
        else
            _navMeshAgent.speed *= 1.75f;
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

    public void GiveOrder(GameObject target, Vector3 position)
    {
        if (!IsSelected) return;
        string target_tag = target.tag;

        switch (target_tag)
        {
            case "PollenSource":
                if (gameObject.CompareTag("Worker"))
                {
                    _workerDuty.isFindingRes = true;
                    _workerDuty.WorkingOnGO = target;
                }

                break;

            default:
                if (gameObject.CompareTag("Worker"))
                {
                    _workerDuty.isFindingRes = false;
                    _workerDuty.isGathering = false;
                    _workerDuty.isBuilding = false;
                    _workerDuty.isFindingBuild = false;
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
