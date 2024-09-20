using System;

namespace BugStrategy.Tiles.WarFog
{
    public interface ITileVisibilityModificator
    {
        public bool FogIsVisible { get; }
        
        public event Action<bool> OnStateChanged;
    }
}