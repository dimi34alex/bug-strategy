using System;
using UnityEngine;

namespace BugStrategy
{
    public interface ITarget
    {
        public Transform Transform { get; }
        public TargetType TargetType { get; }
        public AffiliationEnum Affiliation { get; }
        public bool IsActive { get; }
    
        /// <summary>
        /// mean something like: unit die or construction destructed, also it invoke OnDestroy
        /// </summary>
        public event Action<ITarget> OnDeactivation;
    }
}
