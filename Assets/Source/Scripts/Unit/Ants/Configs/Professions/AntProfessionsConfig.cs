using System.Collections.Generic;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    [CreateAssetMenu(fileName = nameof(AntProfessionsConfig), menuName = "Configs/Units/Ants/" + nameof(AntProfessionsConfig))]
    public class AntProfessionsConfig : ScriptableObject
    {
        [SerializeField] private AntProfessionConfigBase defaultProfession;
        [SerializeField] private SerializableDictionary<ProfessionType, List<AntProfessionConfigBase>> data;

        public AntProfessionConfigBase DefaultProfession => defaultProfession;
        
        public bool TryTakeConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config)
        {
            if (!data.ContainsKey(professionType) || data[professionType].Count < (professionRang))
            {
                config = null;
                return false;
            }
            
            config = data[professionType][professionRang];
            return true;
        }
    }
}