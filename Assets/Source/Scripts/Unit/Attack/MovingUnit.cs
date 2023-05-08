using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MovingUnit : UnitBase
{
    public override MiniMapID MiniMapId => MiniMapID.PlayerUnit;

    private Ray ray;
    private RaycastHit hit;
    private UnitPool pool;

    [SerializeField] private NavMeshAgent _navMeshAgent;

    public override UnitType UnitType => UnitType.MovingUnit;
    public Vector3 Velocity => _navMeshAgent.velocity;

    public bool isSelected;

    [SerializeField] private SomeTestAbility_1 _ability1;
    [SerializeField] private SomeTestAbility_2 _ability2;
    
    void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        pool = controller.GetComponent<UnitPool>();
        pool.UnitCreation(gameObject);
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
        if (Input.GetMouseButton(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GiveOrder(hit.point);
            }
        }

        foreach (var abilite in _abilites)
        {
            abilite.OnUpdate(Time.deltaTime);
        }
        
    }

    public void GiveOrder(Vector3 position)
    {
        switch (hit.collider.gameObject.tag)
        {
            case "PollenSource":
                foreach (GameObject unit in pool.units)
                {
                    if (isSelected == true && unit.gameObject.tag == "Worker")
                    {
                        SetDestination(position);
                        unit.GetComponent<WorkerDuty>().isFindingRes = true;

                    }
                }
                break;

            default:
                foreach (GameObject unit in pool.units)
                {
                    if (isSelected == true)
                    {
                        SetDestination(position);
                        if (unit.gameObject.tag == "Worker" && unit.GetComponent<MovingUnit>().isSelected == true)
                        {
                            unit.GetComponent<WorkerDuty>().isFindingRes = false;
                            unit.GetComponent<WorkerDuty>().isGathering = false;
                            unit.GetComponent<WorkerDuty>().isBuilding = false;
                            unit.GetComponent<WorkerDuty>().isFindingBuild = false;
                        }
                    }
                }
                break;
        }
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
