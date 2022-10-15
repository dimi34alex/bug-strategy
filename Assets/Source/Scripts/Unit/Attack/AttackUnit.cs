using UnityEngine;
using UnityEngine.AI;

public abstract class AttackUnit : UnitBase
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    public sealed override UnitType UnitType => UnitType.AttackUnit;
    public Vector3 Velocity => _navMeshAgent.velocity;

    public void SetDestination(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    public void Warp(Vector3 position)
    {
        _navMeshAgent.Warp(position);
    }
}
