using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.UI.Elements
{
    public class ImageAndTextHolder : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;

        public void Show() 
            => gameObject.SetActive(true);

        public void Hide() 
            => gameObject.SetActive(false);

        public void SetData(Sprite newSprite, string newText)
        {
            SetImageSprite(newSprite);
            SetText(newText);
        }

        public void SetImageSprite(Sprite newSprite) 
            => image.sprite = newSprite;
        
        public void SetText(string newText) 
            => text.text = newText;
    }
}