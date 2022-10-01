using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
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
