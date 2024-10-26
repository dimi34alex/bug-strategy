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

        public bool TryTakeStandardAntConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config) 
            => StandardAntData.TryTakeConfig(professionType, professionRang, out config);

        public bool TryTakeBigAntConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config) 
            => BigAntData.TryTakeConfig(professionType, professionRang, out config);

        public bool TryTakeFlyAntConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config) 
            => FlyAntData.TryTakeConfig(professionType, professionRang, out config);
    }
}