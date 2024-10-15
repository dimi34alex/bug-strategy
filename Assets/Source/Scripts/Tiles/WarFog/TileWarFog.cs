using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles.WarFog
{
    public class TileWarFog : MonoBehaviour
    {
        [SerializeField] private Tile tile;

        [Inject] private TileFogVisibilityModificator _visibilityModificator;
        
        private void Start()
        {
            tile.OnVisibilityChanged += UpdateFog;
            _visibilityModificator.OnStateChanged += OnModChanged;
                
            UpdateFog(tile.IsVisible);
        }

        private void OnModChanged(bool fogIsVisibleByMod) 
            => UpdateFog(tile.IsVisible);
        
        private void UpdateFog(bool tileIsVisible)
        {
            if (tileIsVisible) 
                HideFog();
            else
            {
                if (_visibilityModificator.FogIsVisible)
                    ShowFog();
                else
                    HideFog();
            }
        }

        private void ShowFog() 
            => gameObject.SetActive(true);

        private void HideFog() 
            => gameObject.SetActive(false);
    }
}