using UnityEngine;

namespace MiniMapSystem
{
    public abstract class MiniMapIconConfigBase<TMiniMapIcon> : ScriptableObject, ISingleConfig
        where TMiniMapIcon: MiniMapIconBase
    {
        [SerializeField] private MiniMapIconConfiguration<TMiniMapIcon> configuration;
        public MiniMapIconConfiguration<TMiniMapIcon> Configuration => configuration;
    }
}