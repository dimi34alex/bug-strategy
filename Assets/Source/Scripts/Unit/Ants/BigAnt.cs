using Zenject;

namespace BugStrategy.Unit.Ants
{
    public class BigAnt : AntBase
    {
        [Inject] private AntProfessionsConfigsRepository _professionsConfigsRepository;
        
        public override UnitType UnitType => UnitType.AntBig;
        
        protected override AntProfessionConfigBase GetDefaultProfession() 
            => _professionsConfigsRepository.BigAntData.DefaultProfession;
    }
}