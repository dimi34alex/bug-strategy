using BugStrategy.ConfigsRepository;
using BugStrategy.MiniMap.MiniMapIcons;
using UnityEngine;

namespace BugStrategy.MiniMap.MiniMapIconConfigs
{
    public abstract class MiniMapIconConfigBase<TMiniMapIcon> : ScriptableObject, ISingleConfig
        where TMiniMapIcon: MiniMapIconBase
    {
        [SerializeField] private MiniMapIconConfiguration<TMiniMapIcon> configuration;
        public MiniMapIconConfiguration<TMiniMapIcon> Configuration => configuration;
    }
}