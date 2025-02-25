using System;
using BugStrategy.TechnologiesSystem;
using BugStrategy.TechnologiesSystem.Technologies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.UI.Technologies
{
    public class TechnologyButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text nameField;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text cost;
        [SerializeField] private TMP_Text state;
        
        public TechnologyId TechnologyId { get; private set; }
        private ITechnology _technology;

        public event Action OnClick;

        private void Awake()
        {
            button.onClick.AddListener(() => OnClick?.Invoke());
        }

        public void Initialize(ITechnology technology)
        {
            _technology = technology;
            
            TechnologyId = _technology.Id;
            
            _technology.OnDataChanged += UpdateView;
            
            UpdateView();
        }

        private void UpdateView()
        {
            description.text = $"{_technology.Description}";
            var stateText = "Заблокировано";
            if (_technology.Unlocked)
            {
                stateText = _technology.State switch
                {
                    TechnologyState.UnResearched => "Не исследовано",
                    TechnologyState.ResearchProcess => $"{_technology.ResearchTimer.CurrentTime:0.0}/{_technology.ResearchTimer.MaxTime}",
                    TechnologyState.Researched => "Исследовано",
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                description.text = $"{_technology.UnlockRequirements}";
            }

            nameField.text = $"{_technology.TechName}";
            cost.text = $"{_technology.GetCost()}";
            state.text = stateText;
        }
    }
}