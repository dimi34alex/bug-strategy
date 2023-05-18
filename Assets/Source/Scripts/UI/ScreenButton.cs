using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenButton : MonoBehaviour
{
    private Button _screenButton;
    private Image _image;
    private TextMeshProUGUI _text;
    public event Action ScreenButtonClicked;

    private void Awake()
    {
        _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _image = gameObject.GetComponent<Image>();
        _screenButton = gameObject.GetComponent<Button>();
        _screenButton.onClick.AddListener(OnClick);
    }


    protected void OnClick()
    {
        ScreenButtonClickedInvoke();
    }
    
    protected void ScreenButtonClickedInvoke()
    {
        ScreenButtonClicked?.Invoke();
    }
}
