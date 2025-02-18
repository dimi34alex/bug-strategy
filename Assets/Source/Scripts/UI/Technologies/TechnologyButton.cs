using System;
using BugStrategy.TechnologiesSystem;
using BugStrategy.TechnologiesSystem.Technologies;
using BugStrategy.UI.Elements;
using TMPro;
using UnityEngine;

namespace BugStrategy.UI.Technologies
{
    public class TechnologyButton : ButtonProvider
    {
        [SerializeField] private TMP_Text tmpText;
        
        public TechnologyId TechnologyId { get; private set; }
        private string _techName;
        private ITechnology _technology;
        
        public void Initialize(ITechnology technology)
        {
            _technology = technology;
            TechnologyId = _technology.Id;
            _techName = _technology.TechName;
            
            _technology.OnDataChanged += UpdateView;
            
            UpdateView();
        }

        private void UpdateView()
        {
            var stateText = _technology.State switch
            {
                TechnologyState.UnResearched => "Не исследовано",
                TechnologyState.ResearchProcess => $"{_technology.ResearchTimer.CurrentTime:0.0}/{_technology.ResearchTimer.MaxTime}",
                TechnologyState.Researched => "Исследовано",
                _ => throw new ArgumentOutOfRangeException()
            };

            tmpText.text = $"{_techName}" +
                           $"\n[{stateText}]";
        }
    }
}