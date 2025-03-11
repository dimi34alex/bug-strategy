using UnityEngine;

namespace BugStrategy.MiniMap
{
    public class MiniMapObjView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public void Initialize(Sprite sprite, Color color)
        {
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = color;
        }
    }
}