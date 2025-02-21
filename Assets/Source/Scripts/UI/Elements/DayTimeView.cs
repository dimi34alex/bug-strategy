using BugStrategy.DayChanging;
using TMPro;
using UnityEngine;
using Zenject;

namespace BugStrategy.UI.Elements
{
    public class DayTimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeField;

        [Inject] private readonly DayChanger _dayChanger;
        
        private void FixedUpdate() 
            => timeField.text = _dayChanger.GetTime();
    }
}