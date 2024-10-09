using System;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.UI.NewDirectory1
{
    public class TilesBlock : MonoBehaviour
    {
        [SerializeField] private GroundBlock groundBlock;
        [SerializeField] private ConstructionsBlock constructionsBlock;
        [SerializeField] private ResourceSourcesBlock resourceSourcesBlock;
        
        [SerializeField] private BlocksSwitchButton[] blocksSwitchButtons;

        private BlockBase _activeBlock;
        
        private void Awake()
        {
            groundBlock.Hide();
            constructionsBlock.Hide();
            resourceSourcesBlock.Hide();
            
            foreach (var button in blocksSwitchButtons) 
                button.OnClick += ShowBlock;
        }

        private void ShowBlock(TileType tileType)
        {
            BlockBase newActiveBlock = tileType switch
            {
                TileType.Ground => groundBlock,
                TileType.ResourceSources => resourceSourcesBlock,
                TileType.Constructions => constructionsBlock,
                _ => throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null)
            };

            if (_activeBlock != null)
                _activeBlock.Hide();
            
            if (newActiveBlock == _activeBlock)
            {
                _activeBlock = null;
            }
            else
            {
                _activeBlock = newActiveBlock;
                _activeBlock.Show();
            }
        }
        
        public enum TileType
        {
            Ground = 0,
            ResourceSources = 10,
            Constructions = 20
        }
    }
}