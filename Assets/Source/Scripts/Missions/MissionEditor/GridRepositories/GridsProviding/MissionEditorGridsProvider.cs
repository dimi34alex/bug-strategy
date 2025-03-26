using System;
using BugStrategy.Constructions;
using BugStrategy.ResourceSources;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.GridRepositories.GridsProviding
{
    public class MissionEditorGridsProvider : GridProvider
    {
        private readonly EditorConstructionsRepository _editorConstructionsRepository;
        private readonly EditorResourceSourcesRepository _resourceSourcesRepository;
    
        public override event Action<Vector3> OnAdd;
        public override event Action<Vector3> OnRemove;

        public MissionEditorGridsProvider(EditorConstructionsRepository editorConstructionsRepository, EditorResourceSourcesRepository resourceSourcesRepository)
        {
            _editorConstructionsRepository = editorConstructionsRepository;
            _resourceSourcesRepository = resourceSourcesRepository;

            _editorConstructionsRepository.OnAdd += Add;
            _editorConstructionsRepository.OnRemove += Remove;
            
            _resourceSourcesRepository.OnAdd += Add;
            _resourceSourcesRepository.OnRemove += Remove;
        }

        public override void Dispose()
        {
            _editorConstructionsRepository.OnAdd -= Add;
            _editorConstructionsRepository.OnRemove -= Remove;
            
            _resourceSourcesRepository.OnAdd -= Add;
            _resourceSourcesRepository.OnRemove -= Remove;
        }
        
        private void Add(Vector3 position) 
            => OnAdd?.Invoke(position);

        private void Remove(Vector3 position) 
            => OnRemove?.Invoke(position);
    }
}