using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.MiniMap.Factories
{
    public abstract class MiniMapIconFactoryBehaviourBase<TMiniMapIconBase, TMiniMapIconConfig> : MonoBehaviour, IMiniMapIconFactoryBehaviour
        where TMiniMapIconBase : MiniMapIconBase
        where TMiniMapIconConfig : MiniMapIconConfigBase<TMiniMapIconBase>
    {
        [Inject] private readonly TMiniMapIconConfig _iconConfig;
        public abstract MiniMapIconID MiniMapIconID { get; }

        private void Awake() => OnInit();
        protected virtual void OnInit(){}

        public TMiniMapIcon Create<TMiniMapIcon>() where TMiniMapIcon : MiniMapIconBase
        {
            MiniMapIconConfiguration<TMiniMapIconBase> configuration = _iconConfig.Configuration;
    
            TMiniMapIconBase icon = Instantiate(configuration.iconPrefab,
                configuration.iconPrefab.transform.position, configuration.rotation);
    
            return icon.Cast<TMiniMapIcon>();
        }
    }
}