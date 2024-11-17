using Zenject;

namespace BugStrategy.Unit.Ants
{
    public class FlyAnt : AntBase
    {
        [Inject] private AntProfessionsConfigsRepository _professionsConfigsRepository;

        public override UnitType UnitType => UnitType.AntFlying;
        protected override AntProfessionConfigBase GetDefaultProfession()
            => _professionsConfigsRepository.FlyAntData.DefaultProfession;
    }
}