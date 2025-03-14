using System;
using BugStrategy.Constructions;
using BugStrategy.MiniMap;
using BugStrategy.ResourcesSystem;
using BugStrategy.Trigger;
using UnityEngine;
using Zenject;

namespace BugStrategy.ResourceSources
{
    public abstract class ResourceSourceBase : MonoBehaviour, ITriggerable, ITarget
    {
        [field: SerializeField] public ObjectView View { get; private set; }
        [SerializeField] private int resourceCapacity;

        [Inject] private readonly MiniMapObjViewFactory _miniMapObjViewFactory;
        
        protected FloatStorage ResourceStorage;
    
        public abstract ResourceID ResourceID { get; }
        public bool IsActive { get; protected set; } = true;
        public bool CanBeCollected { get; protected set; } = true;
    
        public AffiliationEnum Affiliation => AffiliationEnum.None;
        public TargetType TargetType => TargetType.ResourceSource;
        public Transform Transform => transform;
    
        public event Action<ITriggerable> OnDisableITriggerableEvent;
        public event Action<ITarget> OnDeactivation;

        private void Awake()
        {
            ResourceStorage = new FloatStorage(resourceCapacity, resourceCapacity);
            _miniMapObjViewFactory.CreateResourceSourceIcon(transform, Affiliation);
        }

        protected virtual void OnAwake(){}
    
        public abstract void ExtractResource(int extracted);
    
        private void OnDisable()
        {
            IsActive = false;
            OnDisableITriggerableEvent?.Invoke(this);
        }

        private void OnDestroy()
        {
            OnDeactivation?.Invoke(this);
        }
    }
}