using System;
using BugStrategy.Ai.InternalAis;
using BugStrategy.Effects;
using BugStrategy.EntityState;
using BugStrategy.Libs;
using BugStrategy.MiniMap;
using BugStrategy.Pool;
using BugStrategy.SelectableSystem;
using BugStrategy.Tiles;
using BugStrategy.Tiles.WarFog;
using BugStrategy.Trigger;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.UnitSelection;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace BugStrategy.Unit
{
    public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable, ITarget,
        ISelectable, Pool.IPoolable<UnitBase, UnitType>, IPoolEventListener, IHealable, IAffiliation,
        IEffectable, IPoisonEffectable, IStickyHoneyEffectable, IMoveSpeedChangeEffectable
    {
        [Inject] private readonly EffectsFactory _effectsFactory;
        [Inject] private readonly MiniMapObjViewFactory _miniMapObjViewFactory;

        private NavMeshAgent _navMeshAgent;
        
        protected FloatStorage _healthStorage  = new FloatStorage(100, 100);
        protected EntityStateMachine _stateMachine;

        public bool IsSticky { get; private set; }
        public bool IsSelected { get; private set; }
        public bool IsActive { get; protected set; }
        public Vector3 TargetMovePosition { get; protected set; }
        public EffectsProcessor EffectsProcessor { get; protected set; }
        public MoveSpeedChangerProcessor MoveSpeedChangerProcessor { get; protected set; }
        public AffiliationEnum Affiliation { get; private set; }
        public abstract InternalAiBase InternalAi { get; protected set; }
        
        protected abstract OrderValidatorBase OrderValidator { get; }
        protected VisibleWarFogZone VisibleWarFogZone { get; private set; }
        
        public bool IsAlive => IsActive && _healthStorage.CurrentValue > 0f;
        public Transform Transform => transform;
        public UnitInteractionZone UnitInteractionZone { get; private set; }
        public UnitInteractionZone DynamicUnitZone { get; private set; }

        public TargetType TargetType => TargetType.Unit;
        public IReadOnlyFloatStorage HealthStorage => _healthStorage;
        public EntityStateMachine StateMachine => _stateMachine;
        public UnitType Identifier => UnitType;
        public abstract FractionType Fraction { get; }
        public abstract UnitType UnitType { get; }
        
        /// <summary> Rodion: dont use it in code. This variable use to look state via inspector </summary>
        public EntityStateID EntityStateID { get; private set; }

        private UnitPathData _currentPathData = new UnitPathData(null, UnitPathType.Idle);
        public UnitPathData CurrentPathData
        {
            get => _currentPathData;
            protected set
            {
                if (value == _currentPathData) 
                    return;

                if (_currentPathData != null && !_currentPathData.Target.IsAnyNull())
                    _currentPathData.Target.OnDeactivation -= OnPathTargetDeactivated;
                
                _currentPathData = value;
                if (_currentPathData != null && !_currentPathData.Target.IsAnyNull())
                    _currentPathData.Target.OnDeactivation += OnPathTargetDeactivated;

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
        /// <remarks> It will also automatically call <see cref="OnDeactivation"/> </remarks>
        public event Action<UnitBase> OnUnitDeactivation;
        /// <remarks> Dont call it, call <see cref="OnUnitDeactivation"/>, it will also automatically call <see cref="OnDeactivation"/> </remarks>
        public event Action<ITarget> OnDeactivation;
        public event Action TookDamage;
        /// <returns> value can be null </returns>
        public event Action<ITarget> TookDamageWithAttacker;
        public event Action PathTargetDeactivated;

        private void Awake()
        {
            _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

            MoveSpeedChangerProcessor = new MoveSpeedChangerProcessor(_navMeshAgent);
            EffectsProcessor = new EffectsProcessor(this, _effectsFactory);

            OnUnitDeactivation += unit => OnDeactivation?.Invoke(unit);

            VisibleWarFogZone = GetComponentInChildren<VisibleWarFogZone>();
            if (VisibleWarFogZone == null) 
                VisibleWarFogZone = CreateSphereTriggerZone<UnitVisibleWarFogZone>();

            if (UnitInteractionZone == null) 
                UnitInteractionZone = CreateSphereTriggerZone<UnitInteractionZone>();

            if (DynamicUnitZone == null) 
                DynamicUnitZone = CreateSphereTriggerZone<UnitInteractionZone>();
            
            OnAwake();
        }

        private void Start()
        {
            UnitPool.Instance.UnitCreation(this);

            OnStart();
        }

        protected virtual void OnAwake() { }

        protected virtual void OnStart() { }
    
        public virtual void HandleUpdate(float time)
        {
            _stateMachine.OnUpdate();
      
            EffectsProcessor.HandleUpdate(time);
        }
    
        private void OnDisable()
        {
            OnDisableITriggerableEvent?.Invoke(this);
        }

        private void OnDestroy()
        {
            CurrentPathData = null;//Rodion: need cus on game destroying target will be deactivated,
            //so it triggered this unit, that destroyed too
            
            OnUnitDeactivation?.Invoke(this);
            ElementDestroyEvent?.Invoke(this);
        }

        public virtual void OnElementReturn()
        {
            IsActive = false;
            CurrentPathData = null;
			StateMachine.SetState(EntityStateID.Idle);
			OnUnitDeactivation?.Invoke(this);
            gameObject.SetActive(false);
        }

        public virtual void OnElementExtract()
        {
            IsActive = true;
            gameObject.SetActive(true);
            EffectsProcessor.Reset();
            MoveSpeedChangerProcessor.Reset();

            SwitchSticky(false);
            
            InternalAi.Reset();
        }
    
        public virtual void Initialize(AffiliationEnum affiliation)
        {
            Affiliation = affiliation;
            _miniMapObjViewFactory.CreateUnitIcon(transform, Affiliation);

            TargetMovePosition = transform.position;
            _navMeshAgent.SetDestination(TargetMovePosition);
        }

        protected void ReturnInPool()
            =>ElementReturnEvent?.Invoke(this);
    
        public void SetDestination(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }

        public void Warp(Vector3 position)
        {
            _navMeshAgent.Warp(position);
        }

        private void OnPathTargetDeactivated(ITarget _) 
            => PathTargetDeactivated?.Invoke();

        public void TakeDamage(IDamageApplicator damageApplicator, float damageScale)
            => TakeDamage(null, damageApplicator, damageScale);
    
        public virtual void TakeDamage(ITarget attacker, IDamageApplicator damageApplicator, float damageScale = 1)
        {
            if (!IsAlive)
            {
                Debug.LogError($"You try damage unit that already dead: {attacker} | {damageApplicator} | {this}");
                return;
            }

            _healthStorage.ChangeValue(-damageApplicator.Damage * damageScale);
            TookDamage?.Invoke();
            TookDamageWithAttacker?.Invoke(attacker);

            if (!IsAlive)
            {
                OnUnitDied?.Invoke(this);
                OnUnitDiedEvent?.Invoke();
                ReturnInPool();
            }
        }

        public void TakeHeal(float value)
            => _healthStorage.ChangeValue(value);

        public void Select()
        {
            if (IsSelected) return;

            IsSelected = true;
            OnSelect?.Invoke();
        }

        public void Deselect()
        {
            if (!IsSelected) return;

            IsSelected = false;
            OnDeselect?.Invoke();
        }

        public void AutoGiveOrder(ITarget target)
            => AutoGiveOrder(target, transform.position);

        /// <param name="targetMovePosition"> move position that used if unitTarget is null</param>
        public void AutoGiveOrder(ITarget target, Vector3 targetMovePosition)
        {
            targetMovePosition.y = 0;

            CurrentPathData = OrderValidator.AutoGiveOrder(target);
            if (!CurrentPathData.Target.IsAnyNull())
            {
                targetMovePosition = OrderValidator.CheckDistance(CurrentPathData)
                    ? transform.position
                    : target.Transform.position;
            }

            CalculateNewState(targetMovePosition);
        }

        public void HandleGiveOrder(ITarget target, UnitPathType unitPathType)
            => HandleGiveOrder(target, unitPathType, transform.position);

        /// <param name="targetMovePosition"> move position that used if unitTarget is null</param>
        public void HandleGiveOrder(ITarget target, UnitPathType unitPathType, Vector3 targetMovePosition)
        {
            targetMovePosition.y = 0;

            CurrentPathData = OrderValidator.HandleGiveOrder(target, unitPathType);
            if (!CurrentPathData.Target.IsAnyNull())
            {
                targetMovePosition = OrderValidator.CheckDistance(CurrentPathData)
                    ? transform.position
                    : target.Transform.position;
            }

            CalculateNewState(targetMovePosition);
        }
        
        private void CalculateNewState(Vector3 newTargetMovePosition)
        {
            newTargetMovePosition.y = 0;
            if (TargetMovePosition != newTargetMovePosition )
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
                    UnitPathType.Repair_Construction => EntityStateID.Repair,
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

        public void SwitchSticky(bool isSticky)
        {
            IsSticky = isSticky;
        }
        
        protected T CreateSphereTriggerZone<T>(bool isTrigger = true) where T : MonoBehaviour
        {
            var holderName = typeof(T).Name;
            var unitInteractionZoneHolder =
                new GameObject
                {
                    name = $"{holderName}Holder",
                    transform = { parent = transform }
                };

            var sphereZone = unitInteractionZoneHolder.AddComponent<SphereCollider>();
            sphereZone.isTrigger = isTrigger;
            
            return unitInteractionZoneHolder.AddComponent<T>();
        }
    }
}