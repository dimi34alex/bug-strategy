using System;
using BugStrategy.Missions;
using BugStrategy.SelectableSystem;
using BugStrategy.Tiles;
using BugStrategy.Trigger;
using UnityEngine;
using Zenject;

namespace BugStrategy.NotConstructions
{
    public abstract class NotConstructionBase : MonoBehaviour, INotConstruction,
        ITriggerable, ITarget, ISelectable, IAffiliation
    {
        [field: SerializeField] public ObjectView View { get; private set; }
        
        [Inject] protected readonly MissionData MissionData;

        private VisibleWarFogZone _visibleWarFogZone;
        
        public AffiliationEnum Affiliation { get; private set; }
        public abstract FractionType Fraction { get; }
        protected abstract NotConstructionConfigBase ConfigBase { get; }

        public bool IsSelected { get; private set; }
        public bool IsActive { get; protected set; } = true;
        public bool IsAlive => IsActive;
    
        public abstract NotConstructionID NotConstructionID { get; }
        public TargetType TargetType => TargetType.NotConstruction;
        public Transform Transform => transform;
    
        protected event Action _updateEvent;
        protected event Action _onDestroy;
        public event Action Initialized;
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