using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.NotConstructions;
using BugStrategy.Factories;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Tiles.WarFog.NewDirectory1
{
    [CreateAssetMenu(fileName = nameof(NotConstructionViewsConfig), menuName = "Configs/NotConstructions/" + nameof(NotConstructionViewsConfig))]
    public class NotConstructionViewsConfig : ScriptableObject, IFactoryConfig<NotConstructionID, ObjectView>, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<NotConstructionID, ObjectView> data;

        public IReadOnlyDictionary<NotConstructionID, ObjectView> GetData() => data;
    }
}