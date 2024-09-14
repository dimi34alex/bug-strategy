using System;
using BugStrategy.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.MiniMap
{
    public abstract class MiniMapIconBase : MonoBehaviour, IPoolable<MiniMapIconBase, MiniMapIconID>
    {
        public abstract MiniMapIconID Identifier { get; }
        public event Action<MiniMapIconBase> ElementReturnEvent;
        public event Action<MiniMapIconBase> ElementDestroyEvent;

        [SerializeField] private Image Image;

        public void SetIconSprite(Sprite sprite)
        {
            Image.sprite = sprite;
        }

        public void Return()
        {
            ElementReturnEvent?.Invoke(this);
        }

        private void OnDestroy()
        {
            ElementDestroyEvent?.Invoke(this);
        }
    }
}