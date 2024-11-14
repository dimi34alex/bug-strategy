using UnityEngine;

namespace BugStrategy
{
    public class ObjectView : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        
        public Sprite Sprite => spriteRenderer.sprite;
        public Transform SkinTransform => spriteRenderer.transform;
        
        /// <param name="sprite"> can be null </param>
        public void SetView(Sprite sprite)
        {
            if (sprite == null)
                return;

            spriteRenderer.sprite = sprite;
        }
    }
}