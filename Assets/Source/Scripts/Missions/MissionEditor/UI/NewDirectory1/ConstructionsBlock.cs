using BugStrategy.Constructions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.MissionEditor.UI.NewDirectory1
{
    public class ConstructionsBlock : BlockBase
    {
        [SerializeField] private ButtonsInitializer<ConstructionID> beeTiles;
        [SerializeField] private ButtonsInitializer<ConstructionID> antTiles;
        [SerializeField] private ButtonsInitializer<ConstructionID> butterflyTiles;

        [Inject] private MissionEditorBuilder _builder;
        [Inject] private MissionEditorConfig _config;

        private void Awake()
        {
            beeTiles.Initialize(_config.Constructions[FractionType.Bees]);
            antTiles.Initialize(_config.Constructions[FractionType.Ants]);
            butterflyTiles.Initialize(_config.Constructions[FractionType.Butterflies]);
            
            beeTiles.OnPressed += Build;
            antTiles.OnPressed += Build;
            butterflyTiles.OnPressed += Build;
        }

        private void Build(ConstructionID id)
            => _builder.ActivateConstructions(id);
    }
}