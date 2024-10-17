using System;
using BugStrategy.Constructions.Factory;
using BugStrategy.Libs;
using BugStrategy.Missions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions
{
    public class InitialConstructions : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<AffiliationEnum, InitPair[]> initUnits;

        [Inject] private readonly MissionData _missionData;
        [Inject] private readonly IConstructionFactory _constructionFactory;
        
        private void Start()
        {
            foreach (var initUnitsArray in initUnits)
            {
                foreach (var initUnitPair in initUnitsArray.Value)
                {
                    Vector3 position = _missionData.ConstructionsRepository.RoundPositionToGrid(initUnitPair.Transform.position);
                    CreateConstruction(initUnitPair.ID, position, initUnitsArray.Key);
                }
            }
        }
        
        private void CreateConstruction(ConstructionID constructionID, Vector3 position, AffiliationEnum affiliation) 
            => _constructionFactory.Create<ConstructionBase>(constructionID, position, affiliation);

        [Serializable]
        private struct InitPair
        {
            [field: SerializeField] public ConstructionID ID { get; private set; }
            [field: SerializeField] public Transform Transform { get; private set; }
        }
    }
}