using System;
using BugStrategy.MiniMap;
using BugStrategy.Missions;
using BugStrategy.Trigger;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions
{
    public abstract class ConstructionBase : MonoBehaviour, IConstruction, IDamagable, IRepairable, IMiniMapObject,
        ITriggerable, ITarget, SelectableSystem.ISelectable, IAffiliation
    {
        [field: SerializeField] public ObjectView View { get; private set; }
        
        [Inject] protected readonly MissionData MissionData;
    
        public AffiliationEnum Affiliation { get; private set; }
        public abstract FractionType Fraction { get; }

        protected readonly FloatStorage _healthStorage = new(0,0);
        
        public bool IsSelected { get; private set; }
        public bool IsActive { get; protected set; } = true;
        public bool IsAlive => IsActive && _healthStorage.CurrentValue > 0f;
    
        public abstract ConstructionID ConstructionID { get; }
        public TargetType TargetType => TargetType.Construction;
        public MiniMapObjectType MiniMapObjectType => MiniMapObjectType.Construction;
        public Transform Transform => transform;
        public IReadOnlyFloatStorage HealthStorage => _healthStorage;
    
        protected event Action _updateEvent;
        protected event Action _onDestroy;
        public event Action Initialized;
        public event Action OnDestruction;
        public event Action<ITarget> OnDeactivation;
        public event Action<ITriggerable> OnDisableITriggerableEvent;
        public event Action OnSelect;
        public event Action OnDeselect;
    
        protected void Awake() => OnAwake();
        protected void Start() => OnStart();
        protected void Update() => _updateEvent?.Invoke();

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }

        public void Initialize(AffiliationEnum newAffiliation)
        {
            Affiliation = newAffiliation;
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
                MissionData.ConstructionsRepository.GetConstruction(transform.position, true);
                OnDestruction?.Invoke();
                Destroy(gameObject);
            }
        }

        public void Demolition()
        {
            IsActive = false;
            MissionData.ConstructionsRepository.GetConstruction(transform.position, true);
            SendDeactivateEvent();
            Destroy(gameObject);

            if(ConstructionID != ConstructionID.BeeWaxTower) OnDestruction?.Invoke();
        }

        public virtual void TakeRepair(IRepairApplicator repairApplicator)
        {
            _healthStorage.ChangeValue(repairApplicator.Rapair);
        }

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

        protected void SendDeactivateEvent() 
            => OnDeactivation?.Invoke(this);
    }
}