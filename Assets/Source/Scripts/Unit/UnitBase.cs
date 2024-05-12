using System;
using System.Collections.Generic;
using UnityEngine;
using MiniMapSystem;
using Unit;
using Unit.Effects;
using Unit.Effects.InnerProcessors;
using Unit.Effects.Interfaces;
using Unit.OrderValidatorCore;

public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable, IUnitTarget, IMiniMapObject,
    SelectableSystem.ISelectable, IPoolable<UnitBase, UnitType>, IPoolEventListener, 
    IHealable, IAffiliation,
    IEffectable, IPoisonEffectable, IStickyHoneyEffectable, IMoveSpeedChangeEffectable
{
    [SerializeField] private UnitVisibleZone _unitVisibleZone;
    [SerializeField] private UnitInteractionZone unitInteractionZone;
    [SerializeField] private UnitInteractionZone dynamicUnitZone;

    [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    [SerializeField] private SomeTestAbility_1 _ability1;
    [SerializeField] private SomeTestAbility_2 _ability2;

    protected ResourceStorage _healthStorage { get; set; } = new ResourceStorage(100, 100);
    protected EntityStateMachine _stateMachine;
    protected List<AbilityBase> _abilites = new List<AbilityBase>();
    
    public bool IsSticky { get; private set; }
    public bool IsSelected { get; private set; }
    public bool IsActive { get; protected set; }
    public Vector3 TargetMovePosition { get; protected set; }
    protected abstract OrderValidatorBase OrderValidator { get; }
    public EffectsProcessor EffectsProcessor { get; protected set; }
    public MoveSpeedChangerProcessor MoveSpeedChangerProcessor { get; protected set; }
    public AffiliationEnum Affiliation { get; private set; }

    public bool IsDied => _healthStorage.CurrentValue < 1f;
    public Transform Transform => transform;
    public UnitVisibleZone VisibleZone => _unitVisibleZone;
    public UnitInteractionZone UnitInteractionZone => unitInteractionZone;
    public UnitInteractionZone DynamicUnitZone => dynamicUnitZone;
    public UnitTargetType TargetType => UnitTargetType.Other_Unit;
    public MiniMapObjectType MiniMapObjectType => MiniMapObjectType.Unit;
    public IReadOnlyResourceStorage HealthStorage => _healthStorage;
    public IReadOnlyList<AbilityBase> Abilities => _abilites;
    public EntityStateMachine StateMachine => _stateMachine;
    public UnitType Identifier => UnitType;
    public abstract FractionType Fraction { get; }
    public abstract UnitType UnitType { get; }
    
    private UnitPathData _currentPathData = new UnitPathData(null, UnitPathType.Idle);
    public UnitPathData CurrentPathData
    {
        get => _currentPathData;
        protected set
        {
            if (value == _currentPathData) return;

            if (!_currentPathData.Target.IsAnyNull())
                _currentPathData.Target.OnDeactivation -= ResetTarget;
            
            _currentPathData = value;
            if (!_currentPathData.Target.IsAnyNull())
                _currentPathData.Target.OnDeactivation += ResetTarget;

            OnUnitPathChange?.Invoke(this);
        }
    }
    
    public event Action<UnitBase> OnUnitPathChange;
    public event Action<UnitBase> OnUnitDied;
    public event Action OnUnitDiedEvent;
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    public event Action OnSelect;
    public event Action OnDeselect;
    public event Action<UnitBase> ElementReturnEvent;
    public event Action<UnitBase> ElementDestroyEvent;
    public event Action OnTargetMovePositionChange;
    public event Action OnDeactivation;

    public void SetAffiliation(AffiliationEnum affiliation)
    {
        Affiliation = affiliation;
    }
    
    public virtual void HandleUpdate(float time)
    {
        _stateMachine.OnUpdate();
    }
    
    public virtual void TakeDamage(IDamageApplicator damageApplicator, float damageScale = 1)
    {
        _healthStorage.ChangeValue(-damageApplicator.Damage * damageScale);
        OnDamaged();

        if (IsDied)
        {
            Debug.Log("���� " + this.gameObject.name + " �������� ");
            OnUnitDied?.Invoke(this);
            OnUnitDiedEvent?.Invoke();
            ReturnInPool();
            return;
        }
    }
    
    public void TakeHeal(float value)
        => _healthStorage.ChangeValue(value);
    
    protected virtual void OnDamaged() { }

    public void Select()
    {
        if(IsSelected) return;
        
        IsSelected = true;
        OnSelect?.Invoke();
    }

    public void Deselect()
    {
        if(!IsSelected) return;
        
        IsSelected = false;
        OnDeselect?.Invoke();
    }

    public virtual void OnElementReturn()
    {
        IsActive = false;
        OnDeactivation?.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void OnElementExtract()
    {
        IsActive = true;
        gameObject.SetActive(true);
        EffectsProcessor.Reset();
        MoveSpeedChangerProcessor.Reset();
        
        SwitchSticky(false);
    }
    
    public void AutoGiveOrder(IUnitTarget unitTarget) 
        => AutoGiveOrder(unitTarget, transform.position);

    /// <param name="targetMovePosition"> move position that used if unitTarget is null</param>
    public void AutoGiveOrder(IUnitTarget unitTarget, Vector3 targetMovePosition)
    {
        targetMovePosition.y = 0;
        
        CurrentPathData = OrderValidator.AutoGiveOrder(unitTarget);
        if (!CurrentPathData.Target.IsAnyNull())
        {
            targetMovePosition = OrderValidator.CheckDistance(CurrentPathData) 
                ? transform.position 
                : unitTarget.Transform.position;
        }

        CalculateNewState(targetMovePosition);
    }

    public void HandleGiveOrder(IUnitTarget unitTarget, UnitPathType unitPathType) 
        => HandleGiveOrder(unitTarget, unitPathType, transform.position);

    /// <param name="targetMovePosition"> move position that used if unitTarget is null</param>
    public void HandleGiveOrder(IUnitTarget unitTarget, UnitPathType unitPathType, Vector3 targetMovePosition)
    {
        targetMovePosition.y = 0;

        CurrentPathData = OrderValidator.HandleGiveOrder(unitTarget, unitPathType);
        if (!CurrentPathData.Target.IsAnyNull())
        {
            targetMovePosition = OrderValidator.CheckDistance(CurrentPathData) 
                ? transform.position 
                : unitTarget.Transform.position;
        }

        CalculateNewState(targetMovePosition);
    }

    private void ResetTarget() 
        => CurrentPathData = new UnitPathData(null, CurrentPathData.PathType);

    public EntityStateID EntityStateID;
    private void CalculateNewState(Vector3 newTargetMovePosition)
    {
        newTargetMovePosition.y = 0;
        if(TargetMovePosition != newTargetMovePosition)
        {
            TargetMovePosition = newTargetMovePosition;
            OnTargetMovePositionChange?.Invoke();
        }
            
        var curPos = transform.position;
        curPos.y = 0;
        if (TargetMovePosition == curPos)
        {
            var newState = CurrentPathData.PathType switch
            {
                UnitPathType.Attack => EntityStateID.Attack,
                UnitPathType.Collect_Resource => EntityStateID.ExtractionResource,
                UnitPathType.Storage_Resource => EntityStateID.StorageResource,
                UnitPathType.Build_Construction => EntityStateID.Build,
                UnitPathType.Move => EntityStateID.Idle,
                UnitPathType.Switch_Profession => EntityStateID.SwitchProfession,
                UnitPathType.Repair_Construction => throw new NotImplementedException(),
                UnitPathType.HideInConstruction => EntityStateID.HideInConstruction,
                _ => throw new NotImplementedException()
            };

            StateMachine.SetState(newState);
        }
        else
        {
            StateMachine.SetState(EntityStateID.Move);
        }

        EntityStateID = _stateMachine.ActiveState;
    }
    
    protected void ReturnInPool()
        => ElementReturnEvent?.Invoke(this);
    
    private void OnDisable()
    {
        OnDisableITriggerableEvent?.Invoke(this);
    }

    public void SwitchSticky(bool isSticky)
    {
        IsSticky = isSticky;
    }
}