using BugStrategy.ResourceSources;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.MissionEditor.UI.NewDirectory1
{
    public class ResourceSourcesBlock : BlockBase
    {
        [SerializeField] private ButtonsInitializer<int> tiles;
        
        [Inject] private MissionEditorBuilder _builder;
        [Inject] private ResourceSourceFactory _factory;

        private void Awake()
        {
            tiles.Initialize(_factory.GetKeys());
            tiles.OnPressed += Build;
        }

        private void Build(int id) 
            => _builder.ResourceSourcePrep(id);
    }
}