using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.TechnologiesSystem.Technologies.Configs
{
    [CreateAssetMenu(fileName = nameof(TechnologiesConfigRepository),
        menuName = "Configs/Technologies/" + nameof(TechnologiesConfigRepository))]
    public class TechnologiesConfigRepository : ScriptableObject, ISingleConfig
    {
        [SerializeField] private List<TechnologyConfig> configs;
        
        public IReadOnlyList<TechnologyConfig> Configs => configs;
    }
}