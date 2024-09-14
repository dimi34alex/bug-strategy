using System.Collections.Generic;
using System.Linq;
using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.ResourcesSystem.ResourcesGlobalStorage
{
    [CreateAssetMenu (fileName = nameof(TeamsResourceGlobalStorageInitialStateConfig), menuName = "Configs/" + nameof(TeamsResourceGlobalStorageInitialStateConfig))]
    public class TeamsResourceGlobalStorageInitialStateConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField]
        private SerializableDictionary<AffiliationEnum, SerializableDictionary<ResourceID, ResourceInitialState>> initialStates;

        public IReadOnlyDictionary<AffiliationEnum, IReadOnlyDictionary<ResourceID, ResourceInitialState>> InitialStates => 
            initialStates.ToDictionary(pair => pair.Key, pair => pair.Value as IReadOnlyDictionary<ResourceID, ResourceInitialState>);
    }
}