using UnityEngine;
using UnityEngine.AI;

public abstract class MovingUnit : UnitBase
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private SomeTestAbility_1 _ability1;
    [SerializeField] private SomeTestAbility_2 _ability2;

    private int _containsStickyTilesCount;
    private float _startMaxSpeed;

    public Vector3 Velocity => _navMeshAgent.velocity;
   
    private void Awake()
    {
        _abilites.Add(_ability1);
        _abilites.Add(_ability2);

        _startMaxSpeed = _navMeshAgent.speed;
    }

    private void Start()
    {
        UnitPool.Instance.UnitCreation(this);
    }

    public void SetDestination(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    public void Warp(Vector3 position)
    {
        _navMeshAgent.Warp(position);
    }
        
    public void ChangeContainsStickyTiles(int delta)
    {
        _containsStickyTilesCount += delta;
        Debug.Log(_containsStickyTilesCount);

        if (_containsStickyTilesCount is 0)
            _navMeshAgent.speed = _startMaxSpeed;
        else
            _navMeshAgent.speed *= 1.75f;
    }

    private void Update()
    {
        foreach (var abilite in _abilites)
        {
            abilite.OnUpdate(Time.deltaTime);
        }
    }

    public virtual void GiveOrder(GameObject target, Vector3 position)
    {
        if (!IsSelected) return;

        string target_tag = target.tag;
  
        SetDestination(position);
    }

    public void UseAbility(int number)
    {
        _abilites[number-1].OnUse();
    }
}

public class BeeUnit : MovingUnit
{
    public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
}
    