using System;
using System.Collections.Generic;
using Unit.ProfessionsCore;
using UnityEngine;

namespace Unit.Ants.Configs.Professions
{
    [CreateAssetMenu(fileName = nameof(AntProfessionsConfigsRepository), menuName = "Configs/Units/Ants/" + nameof(AntProfessionsConfigsRepository))]
    public class AntProfessionsConfigsRepository : ScriptableObject, ISingleConfig
    {
        [SerializeField]
        private SerializableDictionary<ProfessionType, List<AntProfessionConfigBase>> data;
        
        private Dictionary<ProfessionType, Dictionary<int, AntProfessionConfigBase>> _data;

        private void OnValidate()
        {
            _data = new Dictionary<ProfessionType, Dictionary<int, AntProfessionConfigBase>>();
            foreach (var pair in data)
            {
                _data.Add(pair.Key, new Dictionary<int, AntProfessionConfigBase>());

                foreach (var config in pair.Value)
                {
                    if (_data[pair.Key].ContainsKey(config.AntProfessionRang.Rang))
                        throw new ArgumentOutOfRangeException($"some configs with key:{pair.Key} have equal ranges");
                
                    _data[pair.Key].Add(config.AntProfessionRang.Rang, config);
                }
            }
        }
        
        public bool TryTakeConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config)
        {
            var antProfessionRang = new AntProfessionRang(professionRang);
            return TryTakeConfig(professionType, antProfessionRang, out config);
        }
        
        public bool TryTakeConfig(ProfessionType professionType, AntProfessionRang professionRang, out AntProfessionConfigBase config)
        {
            if (!_data.ContainsKey(professionType) || !_data[professionType].ContainsKey(professionRang.Rang))
            {
                config = null;
                return false;
            }
            
            config = _data[professionType][professionRang.Rang];
            return true;
        }
    }
}