using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Constructions;
using BugStrategy.Factories;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Tiles.WarFog.NewDirectory1
{
    [CreateAssetMenu(fileName = nameof(ConstructionViewsConfig), menuName = "Configs/Constructions/" + nameof(ConstructionViewsConfig))]
    public class ConstructionViewsConfig : ScriptableObject, IFactoryConfig<ConstructionID, ObjectView>, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<ConstructionID, ObjectView> data;

        public IReadOnlyDictionary<ConstructionID, ObjectView> GetData() => data;
    }
}