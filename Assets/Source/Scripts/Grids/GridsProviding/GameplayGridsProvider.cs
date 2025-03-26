using System;
using BugStrategy.Constructions;
using BugStrategy.ResourceSources;
using UnityEngine;

namespace BugStrategy.Grids.GridsProviding
{
    public class GameplayGridsProvider : GridProvider
    {
        private readonly ConstructionsRepository _constructionsRepository;
        private readonly ResourceSourcesRepository _resourceSourcesRepository;
    
        public override event Action<Vector3> OnAdd;
        public override event Action<Vector3> OnRemove;

        public GameplayGridsProvider(ConstructionsRepository constructionsRepository, ResourceSourcesRepository resourceSourcesRepository)
        {
            _constructionsRepository = constructionsRepository;
            _resourceSourcesRepository = resourceSourcesRepository;

            _constructionsRepository.OnAdd += Add;
            _constructionsRepository.OnRemove += Remove;
            
            _resourceSourcesRepository.OnAdd += Add;
            _resourceSourcesRepository.OnRemove += Remove;
        }

        public override void Dispose()
        {
            _constructionsRepository.OnAdd -= Add;
            _constructionsRepository.OnRemove -= Remove;
            
            _resourceSourcesRepository.OnAdd -= Add;
            _resourceSourcesRepository.OnRemove -= Remove;
        }
        
        private void Add(Vector3 position) 
            => OnAdd?.Invoke(position);

        private void Remove(Vector3 position) 
            => OnRemove?.Invoke(position);
    }
}