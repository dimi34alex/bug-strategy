using System;
using BugStrategy.Constructions;
using BugStrategy.Pool;

namespace BugStrategy.Tiles.WarFog.Shadows
{
    public class TileShadow : ObjectView, IPoolable<TileShadow>
    {
        public event Action<TileShadow> ElementReturnEvent;
        public event Action<TileShadow> ElementDestroyEvent;

        public void CopyView(ObjectView view)
        {
            spriteRenderer.transform.position = view.SkinTransform.position;
            spriteRenderer.transform.localScale = view.SkinTransform.localScale;
            SetView(view.Sprite);
            gameObject.SetActive(true);
        }
        
        public void ManualReturnInPool()
        {
            gameObject.SetActive(false);
            ElementReturnEvent?.Invoke(this);
        }

        private void OnDestroy() 
            => ElementDestroyEvent?.Invoke(this);
    }
}