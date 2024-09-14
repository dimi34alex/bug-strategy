using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Projectiles;
using UnityEngine;

namespace BugStrategy.Constructions.BeeWaxTower
{
    [Serializable]
    public class BeeWaxTowerLevel : ConstructionLevelBase
    {
        [field: SerializeField] public ProjectileType ProjectilesType { get; private set; }
        [field: SerializeField] [field: Range(0, 4)] public int ProjectilesCount { get; private set; }
        [field: SerializeField] [field: Range(0, 1)] public float SpawnPause { get; private set; }
        [field: SerializeField] [field: Range(0, 100)] public float Damage { get; private set; }
        [field: SerializeField] [field: Range(0, 10)] public float Cooldown { get; private set; }
    }
}