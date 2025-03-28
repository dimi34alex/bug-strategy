using System;
using BugStrategy.MiniMap;
using BugStrategy.Missions;
using BugStrategy.SelectableSystem;
using BugStrategy.Tiles;
using BugStrategy.Trigger;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions
{
    public abstract class ConstructionBase : MonoBehaviour, IConstruction, IDamagable, IRepairable,
        ITriggerable, ITarget, ISelectable, IAffiliation
    { 
        [field: SerializeField] public ObjectView View { get; private set; }
        
        [Inject] protected readonly MissionData MissionData;
        [Inject] private readonly IMiniMapObjViewFactory _miniMapObjViewFactory;

        private VisibleWarFogZone _visibleWarFogZone;
        
        public AffiliationEnum Affiliation { get; private set; }
        public abstract FractionType Fraction { get; }
        protected abstract ConstructionConfigBase ConfigBase { get; }

        protected readonly FloatStorage _healthStorage = new(0,0);

		public bool IsSelected { get; private set; }
        public bool IsActive { get; protected set; } = true;
        public bool IsAlive => IsActive && _healthStorage.CurrentValue > 0f;
    
        public abstract ConstructionID ConstructionID { get; }
        public TargetType TargetType => TargetType.Construction;
        public Transform Transform => transform;
        public IReadOnlyFloatStorage HealthStorage => _healthStorage;
    
        protected event Action _updateEvent;
        protected event Action _onDestroy;
        public event Action Initialized;
        public event Action OnDestruction;
        public event Action<ITarget> OnDeactivation;
        public event Action<ITriggerable> OnDisableITriggerableEvent;
        public event Action<bool> OnSelect;
        public event Action OnDeselect;
    
        protected void Awake() => OnAwake();
        protected void Start() => OnStart();
        protected void Update() => _updateEvent?.Invoke();

        protected virtual void OnAwake()
        {
            _visibleWarFogZone = GetComponentInChildren<VisibleWarFogZone>();
            _visibleWarFogZone.SetRadius(ConfigBase.WarFogViewRadius);
        }
        
        protected virtual void OnStart() { }

        public void Initialize(AffiliationEnum newAffiliation)
        {
            Affiliation = newAffiliation;
            _miniMapObjViewFactory.CreateConstructionIcon(transform, Affiliation);
            
            Initialized?.Invoke();
        }
    
        private void OnDisable()
        {
            IsActive = false;
            OnDisableITriggerableEvent?.Invoke(this);
        }
    
        private void OnDestroy()
        {
            IsActive = false;
            _onDestroy?.Invoke();
            OnDeactivation?.Invoke(this);
        }
    
        public virtual void TakeDamage(ITarget attacker, IDamageApplicator damageApplicator, float damageScale = 1)
        {
            if (!IsAlive)
            {
                Debug.LogError($"You try damage construction that already destructed {attacker} | {damageApplicator} | {this}");
                return;
            }

            _healthStorage.ChangeValue(-damageApplicator.Damage * damageScale);
            if (_healthStorage.CurrentValue <= 0)
            {
                IsActive = false;
                OnDeactivation?.Invoke(this);
                MissionData.ConstructionsRepository.Get(transform.position, true);
                OnDestruction?.Invoke();
                Destroy(gameObject);
            }
        }

        public void Demolition()
        {
            IsActive = false;
            MissionData.ConstructionsRepository.Get(transform.position, true);
            SendDeactivateEvent();
            Destroy(gameObject);

            if(ConstructionID != ConstructionID.BeeWaxTower) OnDestruction?.Invoke();
        }

        public virtual void TakeRepair(IRepairApplicator repairApplicator)
        {
            _healthStorage.ChangeValue(repairApplicator.Repair);
        }

        public void Select(bool isFullView)
        {
            if(IsSelected) return;

            IsSelected = true;
            OnSelect?.Invoke(isFullView);
        }

        public void Deselect()
        {
            if(!IsSelected) return;

            IsSelected = false;
            OnDeselect?.Invoke();
        }

        protected void SendDeactivateEvent() 
            => OnDeactivation?.Invoke(this);
    }
}