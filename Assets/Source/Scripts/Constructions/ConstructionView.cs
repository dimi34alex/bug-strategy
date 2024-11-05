using UnityEngine;

namespace BugStrategy.Constructions
{
    public class ConstructionView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        /// <param name="sprite"> can be null </param>
        public void SetView(Sprite sprite)
        {
            if (sprite == null)
                return;

            spriteRenderer.sprite = sprite;
        }
    }
}