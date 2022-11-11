using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingUnit : UnitBase
{
    private Ray ray;
    private RaycastHit hit;
    private UnitPool pool;

    [SerializeField] private NavMeshAgent _navMeshAgent;

    public override UnitType UnitType => UnitType.MovingUnit;
    public Vector3 Velocity => _navMeshAgent.velocity;

    public bool isSelected;

    void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        pool = controller.GetComponent<UnitPool>();
        pool.UnitCreation(gameObject);
    }

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

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GiveOrder(hit.point);
            }
        }
    }

    public void GiveOrder(Vector3 position)
    {
        foreach (GameObject unit in pool.units)
        {
            if (isSelected == true)
            {
                SetDestination(position);
            }
        }
    }
}
