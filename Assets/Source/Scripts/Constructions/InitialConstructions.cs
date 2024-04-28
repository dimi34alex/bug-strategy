using System;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class InitialConstructions : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<AffiliationEnum, InitPair[]> initUnits;

        [Inject] private readonly IConstructionFactory _constructionFactory;
        
        private void Start()
        {
            foreach (var initUnitsArray in initUnits)
            {
                foreach (var initUnitPair in initUnitsArray.Value)
                {
                    Vector3 position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(initUnitPair.Transform.position);
                    CreateConstruction(initUnitPair.ID, position, initUnitsArray.Key);
                }
            }
        }
        
        private void CreateConstruction(ConstructionID constructionID, Vector3 position, AffiliationEnum affiliation)
        {
            ConstructionBase construction = _constructionFactory.Create<ConstructionBase>(constructionID, affiliation);
            
            FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, construction);
            construction.transform.position = position;
        }
        
        [Serializable]
        private struct InitPair
        {
            [field: SerializeField] public ConstructionID ID { get; private set; }
            [field: SerializeField] public Transform Transform { get; private set; }
        }
    }
}