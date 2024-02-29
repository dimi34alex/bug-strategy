using System;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class InitialConstructions : MonoBehaviour
    {
        [SerializeField] private InitPair[] inits;

        [Inject] private readonly IConstructionFactory _constructionFactory;
        
        private void Start()
        {
            foreach (var init in inits)
                CreateConstruction(init.ID, init.Transform.position);
        }
        
        private void CreateConstruction(ConstructionID constructionID, Vector3 position)
        {
            ConstructionBase construction = _constructionFactory.Create<ConstructionBase>(constructionID);
            
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