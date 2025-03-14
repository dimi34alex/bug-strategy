using UnityEngine;

namespace BugStrategy.ObjectAiming
{
    [RequireComponent(typeof(ObjectView))]
    public class SkinOutlineHolder : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer skin;
        [SerializeField] private Material outlineMaterial;

        private ObjectView _objectView;
        private SpriteRenderer _skinOutline;

        private void Awake ()
        {
            _skinOutline = Instantiate(skin, skin.transform.position, skin.transform.rotation, transform);
            _skinOutline.transform.localScale = skin.transform.localScale;
            _skinOutline.transform.name = "SkinOutline";
            _skinOutline.material = outlineMaterial;

            _objectView = GetComponent<ObjectView>();
            _objectView.OnChangeSprite += UpdateSprite;

            ToggleOutlineVisibility(false);
        }

        private void OnDestroy ()
        {
            if(_objectView != null)
                _objectView.OnChangeSprite -= UpdateSprite;
        }

        public void ToggleOutlineVisibility (bool isVisible)
        {
            if(_skinOutline != null)
                _skinOutline.gameObject.SetActive(isVisible);
        }

        private void UpdateSprite (Sprite newSprite) => _skinOutline.sprite = newSprite;

    }
}