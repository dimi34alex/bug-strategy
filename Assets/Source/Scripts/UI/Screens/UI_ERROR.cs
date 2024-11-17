using System.Collections;
using BugStrategy.CustomInput;
using CycleFramework.Screen;
using TMPro;
using UnityEngine;
using Zenject;

namespace BugStrategy.UI.Screens
{
    public class UI_ERROR : UIScreen
    {
        [SerializeField] private GameObject error;

        [Inject] private readonly IInputProvider _inputProvider;
        
        public void ErrorCall(string errorText)
        {
            if(errorText != null)
                StartCoroutine(ErrorInvis(errorText));
        }
    
        IEnumerator ErrorInvis(string errorText)
        {
            GameObject newError = Instantiate(error, _inputProvider.MousePosition, Quaternion.Euler(0,0,0), transform);
        
            TextMeshProUGUI text = newError.GetComponentInChildren<TextMeshProUGUI>();
            text.text = errorText;
        
            for (float alpha = 0f; alpha <= 2; alpha += 0.1f)
                yield return new WaitForSeconds(.1f);
        
            Destroy(newError);
        }
    }
}
