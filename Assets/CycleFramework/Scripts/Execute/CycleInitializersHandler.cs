using System.Collections.Generic;
using CycleFramework.Bootload;
using UnityEngine;

namespace CycleFramework.Execute
{
    [DefaultExecutionOrder(-2000)]
    public class CycleInitializersHandler : MonoBehaviour
    {
        [SerializeField] private CycleState _cycleState;

        public CycleState CycleState => _cycleState;
        public IReadOnlyList<CycleInitializerBase> Initializers { get; private set; }

        private void Awake()
        {
            Initializers = GetComponentsInChildren<CycleInitializerBase>(true);
        }
    }
}
