using System;
using BugStrategy.MiniMap;
using BugStrategy.ResourcesSystem;
using BugStrategy.Trigger;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.ResourceSources
{
    public abstract class ResourceSourceBase : MonoBehaviour, IMiniMapObject, ITriggerable, ITarget
    {
        [SerializeField] private int resourceCapacity;
    
        protected FloatStorage ResourceStorage;
    
        public abstract ResourceID ResourceID { get; }
        public bool IsActive { get; protected set; } = true;
        public bool CanBeCollected { get; protected set; } = true;
    
        public AffiliationEnum Affiliation => AffiliationEnum.None;
        public MiniMapObjectType MiniMapObjectType => MiniMapObjectType.ResourceSource;
        public TargetType TargetType => TargetType.ResourceSource;
        public Transform Transform => transform;
    
        public event Action<ITriggerable> OnDisableITriggerableEvent;
        public event Action<ITarget> OnDeactivation;

        private void Awake()
        {
            ResourceStorage = new FloatStorage(resourceCapacity, resourceCapacity);
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