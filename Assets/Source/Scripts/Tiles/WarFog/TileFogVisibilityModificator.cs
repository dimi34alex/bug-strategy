using System;

namespace BugStrategy.Tiles.WarFog
{
    public class TileFogVisibilityModificator : ITileVisibilityModificator
    {
        public bool FogIsVisible { get; private set; }
        
        public event Action<bool> OnStateChanged;

        public TileFogVisibilityModificator(bool isVisible)
        {
            FogIsVisible = isVisible;
        }
        
        public void SetState(bool fogIsVisible)
        {
            if (FogIsVisible == fogIsVisible)
                return;

            FogIsVisible = fogIsVisible;
            OnStateChanged?.Invoke(FogIsVisible);
        }
    }
}