using System;
using System.Collections.Generic;
using System.Reflection;
using CycleFramework.Bootload;

namespace CycleFramework.Execute
{
    public class CycleEventsRepository
    {
        private readonly Dictionary<CycleState, Dictionary<CycleMethodType, Action>> _events;

        public CycleEventsRepository(IReadOnlyDictionary<CycleMethodType, MethodInfo> methods, 
            IEnumerable<CycleInitializersHandler> initializers)
        {
            _events = new Dictionary<CycleState, Dictionary<CycleMethodType, Action>>();

            foreach (CycleInitializersHandler initializersHandler in initializers)
            {
                _events.Add(initializersHandler.CycleState, new Dictionary<CycleMethodType, Action>());

                foreach (CycleMethodType cycleMethod in methods.Keys)
                {
                    _events[initializersHandler.CycleState].Add(cycleMethod, null);

                    foreach (CycleInitializerBase cycleInitializerBase in initializersHandler.Initializers)
                    {
                        _events[initializersHandler.CycleState][cycleMethod] +=
                            (Action)methods[cycleMethod].CreateDelegate(typeof(Action), cycleInitializerBase);
                    }
                }
            }
        }

        public Action GetAction(CycleState cycleState, CycleMethodType cycleMethodType)
        {
            return _events[cycleState][cycleMethodType];
        }
    }
}
