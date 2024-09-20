using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles.WarFog
{
    public class TileStartWarFog : MonoBehaviour
    {
        [SerializeField] private Tile tile;

        [Inject] private TileFogVisibilityModificator _visibilityModificator;
        
        private bool _isVisible;
        
        private void Start()
        {
            _isVisible = gameObject.activeSelf;
            tile.OnVisibilityChanged += UpdateFog;
            _visibilityModificator.OnStateChanged += OnModChanged;
                
            UpdateFog(tile.IsVisible);
        }

        private void OnModChanged(bool fogIsVisibleByMod) 
            => UpdateFog(tile.IsVisible);
        
        private void UpdateFog(bool tileIsVisible)
        {
            if (!_isVisible)
                return;

            if (tileIsVisible)
            {
                HideFog();
                _isVisible = false;
            }
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