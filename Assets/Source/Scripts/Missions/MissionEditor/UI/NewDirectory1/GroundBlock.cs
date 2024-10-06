using BugStrategy.Tiles;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BugStrategy.Missions.MissionEditor.UI.NewDirectory1
{
    public class GroundBlock : BlockBase
    {
        [SerializeField] private ButtonsInitializer<int> tiles;
        [SerializeField] private Button clear;

        [Inject] private MissionEditorBuilder _builder;
        [Inject] private TilesFactory _factory;

        private void Awake()
        {
            tiles.Initialize(_factory.GetKeys());
            tiles.OnPressed += Build;
        }

        private void Build(int id) 
            => _builder.TilePrep(id);
    }
}