
using CycleFramework.Bootload;

namespace CycleFramework.Execute
{
    public class CycleEventsProcessor
    {
        private readonly CycleEventsRepository _cycleEventsRepository;

        public CycleEventsProcessor(CycleEventsRepository cycleEventsRepository)
        {
            _cycleEventsRepository = cycleEventsRepository;
        }

        public void Execute(CycleState cycleState, CycleMethodType cycleMethodType)
        {
            _cycleEventsRepository.GetAction(cycleState, cycleMethodType)?.Invoke();
        }
    }
}