using BugStrategy.Missions;
using BugStrategy.TechnologiesSystem;
using BugStrategy.TechnologiesSystem.Technologies;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BugStrategy.UI.Technologies
{
    public class TechnologyWindow : MonoBehaviour
    {
        [SerializeField] private MissionTechnologiesConfig missionConfig;
        [SerializeField] private TechnologyButton buttonPrefab;
        [SerializeField] private RectTransform buttonsHolder;
        [SerializeField] private GameObject techWindow;
        [SerializeField] private Button open;
        [SerializeField] private Button close;

        [Inject] private readonly TechnologyModule _technologyModule;
        [Inject] private readonly MissionData _missionData;
        
        private void Awake()
        {
            missionConfig.CheckDuplicates();
            foreach (var technologyId in missionConfig.Technologies)
            {
                var button = Instantiate(buttonPrefab, buttonsHolder);
                button.Initialize(GetTechnology(technologyId));
                button.OnClick += () => TryResearchTechnology(button.TechnologyId);
            }
            
            open.onClick.AddListener(() => ToggleWindow(true));
            close.onClick.AddListener(() => ToggleWindow(false));
            
            ToggleWindow(false);
        }
        
        private void ToggleWindow(bool isActive) 
            => techWindow.SetActive(isActive);

        private ITechnology GetTechnology(TechnologyId technologyId) =>
            _technologyModule.GetTechnology(_missionData.PlayerAffiliation, technologyId);

        private void TryResearchTechnology(TechnologyId technologyId) 
            => _technologyModule.TryResearchTechnology(_missionData.PlayerAffiliation ,technologyId);
    }
}