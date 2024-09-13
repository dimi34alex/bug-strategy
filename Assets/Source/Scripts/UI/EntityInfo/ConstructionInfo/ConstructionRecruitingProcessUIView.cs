using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Ai.ConstructionsAis;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators;
using Source.Scripts.ResourcesSystem;
using Source.Scripts.UI.EntityInfo.UnitInfo;
using UnityEngine;

namespace Source.Scripts.UI.EntityInfo.ConstructionInfo
{
    public class ConstructionRecruitingProcessUIView : ButtonPanelUIView<int>
    {
        [SerializeField] private BarView _barView;

        private IRecruitingConstruction _recruiter;
        private UIUnitsConfig _uiUnitsConfig;
        private FloatStorage _progressStorage;

        private readonly Dictionary<UnitType, Sprite> _images = new();
        private readonly List<int> _orderedTypes = new() { 0, 1, 2, 3, 4, 5 };
        
        private void Start()
        {
            _uiUnitsConfig = ConfigsRepository.FindConfig<UIUnitsConfig>();
            _progressStorage = new FloatStorage(1, 1);
            InitBar(_progressStorage);

            ButtonClicked += TryCancelRecruiting;

            foreach (var pair in _uiUnitsConfig.UnitsUIConfigs) 
                _images.Add(pair.Key, pair.Value.InfoSprite);
        }

        private void InitBar(IReadOnlyFloatStorage storage)
        {
            _barView.Init(storage);
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
        }

        private void UpdateBarView()
        {
            var recruitingInformation = _recruiter.Recruiter.GetRecruitingInformation();
            if (recruitingInformation.Count <= 0)
                return;

            var recruitingStack = recruitingInformation.First();
            if (recruitingStack != null && !recruitingStack.Empty)
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

        public void ActivateBar() 
            => _barView.gameObject.SetActive(true);

        private void TryCancelRecruiting(int stackIndex) 
            => _recruiter.CancelRecruit(stackIndex);
    }
}
