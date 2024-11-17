using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Unit.Ants;
using Zenject;

namespace BugStrategy.Constructions
{
    public abstract class AntWorkshopBase : ConstructionBase, IEvolveConstruction
    {
        [Inject] private readonly AntProfessionsConfigsRepository _antProfessionsConfigsRepository;

        public abstract ProfessionType ProfessionType { get; }
        public abstract IConstructionLevelSystem LevelSystem { get; protected set; }
        public WorkshopCore WorkshopCore { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            WorkshopCore = new WorkshopCore(ProfessionType, _antProfessionsConfigsRepository);
        }
    }
}