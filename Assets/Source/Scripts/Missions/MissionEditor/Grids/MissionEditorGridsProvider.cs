using System;
using BugStrategy.Grids.GridsProviding;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Grids
{
    public class MissionEditorGridsProvider : GridProvider
    {
        private readonly EditorConstructionsGrid _editorConstructionsGrid;
        private readonly EditorResourceSourcesRepository _resourceSourcesRepository;
    
        public override event Action<Vector3> OnAdd;
        public override event Action<Vector3> OnRemove;

        public MissionEditorGridsProvider(EditorConstructionsGrid editorConstructionsGrid, EditorResourceSourcesRepository resourceSourcesRepository)
        {
            _editorConstructionsGrid = editorConstructionsGrid;
            _resourceSourcesRepository = resourceSourcesRepository;

            _editorConstructionsGrid.OnAdd += Add;
            _editorConstructionsGrid.OnRemove += Remove;
            
            _resourceSourcesRepository.OnAdd += Add;
            _resourceSourcesRepository.OnRemove += Remove;
        }

        public override void Dispose()
        {
            _editorConstructionsGrid.OnAdd -= Add;
            _editorConstructionsGrid.OnRemove -= Remove;
            
            _resourceSourcesRepository.OnAdd -= Add;
            _resourceSourcesRepository.OnRemove -= Remove;
        }
        
        private void Add(Vector3 position) 
            => OnAdd?.Invoke(position);

        private void Remove(Vector3 position) 
            => OnRemove?.Invoke(position);
    }
}