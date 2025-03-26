using UnityEngine;
using Zenject;

namespace BugStrategy.NotConstructions.Factory.Behaviours
{
    public abstract class NotConstructionFactoryBehaviourBase : MonoBehaviour
    {
        [Inject] protected readonly DiContainer DiContainer;

        protected void Awake()
        {
            OnInit();
        }

        protected virtual void OnInit() { }

        public abstract NotConstructionType NotConstructionType { get; }
        public abstract TConstruction Create<TConstruction>(NotConstructionID notConstructionID, Transform parent = null) 
            where TConstruction : NotConstructionBase;
    }
}
