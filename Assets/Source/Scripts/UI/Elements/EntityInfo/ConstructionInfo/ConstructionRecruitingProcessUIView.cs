using System.Collections.Generic;
using System.Linq;
using BugStrategy.Bars;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.Libs;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;
using BugStrategy.UI.Elements.FloatStorageViews;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo
{
    public class ConstructionRecruitingProcessUIView : ButtonPanelUIView<int>
    {
        [SerializeField] private FloatStorageBarView _barView;

        private IRecruitingConstruction _recruiter;
        private UIUnitsConfig _uiUnitsConfig;
        private FloatStorage _progressStorage;

        private readonly Dictionary<UnitType, Sprite> _images = new();
        private readonly List<int> _orderedTypes = new() { 0, 1, 2, 3, 4, 5 };
        
        private void Start()
        {
            _uiUnitsConfig = ConfigsRepository.ConfigsRepository.FindConfig<UIUnitsConfig>();
            _progressStorage = new FloatStorage(1, 1);
            _barView.SetStorage(_progressStorage);

            ButtonClicked += TryCancelRecruiting;

            foreach (var pair in _uiUnitsConfig.UnitsUIConfigs) 
                _images.Add(pair.Key, pair.Value.InfoSprite);
        }

        public void InitRecruiter(IRecruitingConstruction recruiter)
        {
            if (!_recruiter.IsAnyNull())
                _recruiter.Recruiter.OnTick -= UpdateBarView;

            if (recruiter.IsAnyNull())
                return;
            
            _recruiter = recruiter;
            _recruiter.Recruiter.OnTick += UpdateBarView;

            _recruiter.Recruiter.OnChange += UpdateButtons;
            
            UpdateBarView();
            UpdateButtons();
            _barView.gameObject.SetActive(true);
        }

        private void UpdateBarView()
        {
            var recruitingInformation = _recruiter.Recruiter.GetRecruitingInformation();
            if (recruitingInformation.Count <= 0)
            {
                _progressStorage.SetValue(0);
                return;
            }

            var recruitingStack = recruitingInformation.First();
            if (recruitingStack == null || recruitingStack.Empty)
            {
                _progressStorage.SetValue(0);
            }
            else
            {
                var processPercentage = recruitingStack.RecruitingTimer / recruitingStack.RecruitingTime;
                _progressStorage.SetValue(processPercentage);
            }
        }

        private void UpdateButtons()
        {
            var dict = new Dictionary<int, Sprite>(_orderedTypes.Count);
            foreach (var index in _orderedTypes) 
                dict.Add(index, null);

            var recruitingInformation = _recruiter.Recruiter.GetRecruitingInformation();
            for (int i = 0; i < recruitingInformation.Count && i < _orderedTypes.Count; i++)
                if (!recruitingInformation[i].Empty)
                    dict[i] = _images[recruitingInformation[i].UnitId];
            
            SetButtons(false, dict, _orderedTypes);
        }
        
        public void Hide()
        {
            TurnOffButtons();
            _barView.gameObject.SetActive(false);
        }

        private void TryCancelRecruiting(int stackIndex) 
            => _recruiter.CancelRecruit(stackIndex);
    }
}
