﻿using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.BeeStickyTile
{
    [CreateAssetMenu(fileName = nameof(BeeStickyTileSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(BeeStickyTileSpawnConfig))]
    public class BeeStickyTileSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BeeStickyTile> _configuration;

        public ConstructionSpawnConfiguration<BeeStickyTile> GetConfiguration()
        {
            return _configuration;
        }
    }
}