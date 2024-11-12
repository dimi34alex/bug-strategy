using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    [CreateAssetMenu(fileName = nameof(AntProfessionsConfigsRepository), menuName = "Configs/Units/Ants/" + nameof(AntProfessionsConfigsRepository))]
    public class AntProfessionsConfigsRepository : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public AntProfessionsConfig StandardAntData { get; private set; }
        [field: SerializeField] public AntProfessionsConfig BigAntData { get; private set; }
        [field: SerializeField] public AntProfessionsConfig FlyAntData { get; private set; }

        public int GetRangsCount(UnitType unitType, ProfessionType professionType)
        {
            return unitType switch
            {
                UnitType.AntStandard => StandardAntData.GetRangsCount(professionType),
                UnitType.AntBig => BigAntData.GetRangsCount(professionType),
                UnitType.AntFlying => FlyAntData.GetRangsCount(professionType),
                _ => 0
            };
        }

        public bool TryGetAntConfig(UnitType unitType, ProfessionType professionType, int professionRang, out AntProfessionConfigBase config)
        {
            switch (unitType)
            {
                case UnitType.AntStandard:
                    return TryTakeStandardAntConfig(professionType, professionRang, out config);
                case UnitType.AntBig:
                    return TryTakeBigAntConfig(professionType, professionRang, out config);
                case UnitType.AntFlying:
                    return TryTakeFlyAntConfig(professionType, professionRang, out config);
                default:
                    config = null;
                    return false;
            }
        }
        
        public bool TryTakeStandardAntConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config) 
            => StandardAntData.TryTakeConfig(professionType, professionRang, out config);

        public bool TryTakeBigAntConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config) 
            => BigAntData.TryTakeConfig(professionType, professionRang, out config);

        public bool TryTakeFlyAntConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config) 
            => FlyAntData.TryTakeConfig(professionType, professionRang, out config);
    }
}