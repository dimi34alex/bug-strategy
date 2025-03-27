using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.UI.NewDirectory1
{
    public class BlocksSwitchButton : IdButtonProvider<TilesBlock.TileType>
    {
        [SerializeField] private TilesBlock.TileType tileType;

        private void Awake() 
            => Initialize(tileType);

        protected override string GetName(TilesBlock.TileType id) 
            => tmpText.text;
    }
}