using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingUnit : UnitBase
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    public override UnitType UnitType => UnitType.MovingUnit;
    public Vector3 Velocity => _navMeshAgent.velocity;

    private void Awake()
    {
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
}
