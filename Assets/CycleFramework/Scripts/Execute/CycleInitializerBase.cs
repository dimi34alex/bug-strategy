using UnityEngine;

namespace CycleFramework.Execute
{
    public abstract class CycleInitializerBase : MonoBehaviour
    {
        /// <summary>
        /// If you want to rename it, need also rename it in enum CycleMethodType
        /// </summary>
        protected virtual void OnStartInit() { }
        /// <summary>
        /// If you want to rename it, need also rename it in enum CycleMethodType
        /// </summary>
        protected virtual void OnUpdate() { }
        /// <summary>
        /// If you want to rename it, need also rename it in enum CycleMethodType
        /// </summary>
        protected virtual void OnFixedUpdate() { }
    }
}
