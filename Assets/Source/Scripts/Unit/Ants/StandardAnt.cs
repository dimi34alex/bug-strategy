using Zenject;

namespace BugStrategy.Unit.Ants
{
    public class StandardAnt : AntBase
    {
        [Inject] private AntProfessionsConfigsRepository _professionsConfigsRepository;

        public override UnitType UnitType => UnitType.AntStandard;
        
        protected override AntProfessionConfigBase GetDefaultProfession()
            => _professionsConfigsRepository.StandardAntData.DefaultProfession;
    }
}