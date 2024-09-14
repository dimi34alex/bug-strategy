using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.MiniMap.MiniMapIcons.Configs
{
    public abstract class MiniMapIconConfigBase<TMiniMapIcon> : ScriptableObject, ISingleConfig
        where TMiniMapIcon: MiniMapIconBase
    {
        [SerializeField] private MiniMapIconConfiguration<TMiniMapIcon> configuration;
        public MiniMapIconConfiguration<TMiniMapIcon> Configuration => configuration;
    }
}