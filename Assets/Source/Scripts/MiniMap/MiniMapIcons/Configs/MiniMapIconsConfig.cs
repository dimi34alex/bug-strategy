using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Factory;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.MiniMap.MiniMapIcons.Configs
{
    [CreateAssetMenu(fileName = nameof(MiniMapIconsConfig), menuName = "Configs/" + nameof(MiniMapIconsConfig))]
    public class MiniMapIconsConfig : ScriptableObject, IFactoryConfig<MiniMapIconID, MiniMapIconBase>, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<MiniMapIconID, MiniMapIconBase> data;
        
        public IReadOnlyDictionary<MiniMapIconID, MiniMapIconBase> GetData() 
            => data;
    }
}