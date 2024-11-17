using System;
using System.Collections.Generic;
using CycleFramework.Bootload;
using UnityEngine;

namespace CycleFramework.Execute
{
    [DefaultExecutionOrder(1000)]
    public class CycleEventsTransmitter : MonoBehaviour
    {
        private CycleStateMachine _cycleStateMachine;
        private CycleEventsProcessor _cycleEventsProcessor;

        public void Init(CycleStateMachine cycleStateMachine, CycleEventsProcessor cycleEventsProcessor)
        {
            _cycleStateMachine = cycleStateMachine;
            _cycleEventsProcessor = cycleEventsProcessor;
        }

        private void Start()
        {
            if (_cycleEventsProcessor is null)
                throw new NullReferenceException($"{typeof(CycleEventsProcessor)} cannot be null");

            foreach (CycleState cycleState in (IEnumerable<CycleState>)Enum.GetValues(typeof(CycleState)))
                _cycleEventsProcessor.Execute(cycleState, CycleMethodType.OnStartInit);
        }

        private void Update()
        {
            _cycleEventsProcessor.Execute(_cycleStateMachine.CurrentCycleState, CycleMethodType.OnUpdate);
        }

        private void FixedUpdate()
        {
            _cycleEventsProcessor.Execute(_cycleStateMachine.CurrentCycleState, CycleMethodType.OnFixedUpdate);
        }
    }
}
