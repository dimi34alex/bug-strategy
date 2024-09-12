using System.Collections.Generic;
using System.Linq;
using UnitsRecruitingSystemCore;
using UnityEngine;

namespace Source.Scripts.UI.ConstructionInformation
{
    public class ConstructionCreationProductUIView : ButtonPanelUIView<UnitType>
    {
        [SerializeField] private BarView _barView;

        [SerializeField] private GameObject _dopPanel;

        private IReadOnlyUnitsRecruiter _recruiter;

        private ResourceStorage _resourceStorage;


        private void Start()
        {
            _resourceStorage = new ResourceStorage(1,1);
            InitBar(_resourceStorage);
        }

        public void InitBar(IReadOnlyResourceStorage storage)
        {
            _barView.Init(storage);
        }

        public void InitRec(IReadOnlyUnitsRecruiter recruiter)
        {
            if (_recruiter!=null)
                _recruiter.OnTick -= OnUpdateInfo;

            _recruiter = recruiter;
            _recruiter.OnTick += OnUpdateInfo;

            OnUpdateInfo();

        }

        public void OnUpdateInfo()
        {

            var a = _recruiter.GetRecruitingInformation().First();

            Debug.Log(a.RecruitingTime);
            Debug.Log(a.RecruitingTimer);
            if (_recruiter.GetRecruitingInformation().First() != null)
            {
                _resourceStorage.SetCapacity(a.RecruitingTime);
                _resourceStorage.SetValue(a.RecruitingTimer);
            }
        }

        public void CloseAll()
        {
            _dopPanel.SetActive(false);
            base.TurnOffButtons();
            _barView.gameObject.SetActive(false);
        }

        public void ActivateBar()
        {
            _barView.gameObject.SetActive(true);
        }

        public void ActivatePanel()
        {
            _dopPanel.SetActive(true);
        }

        public override void SetButtons(IReadOnlyDictionary<UnitType, Sprite> images, List<UnitType> orderedTypes)
        {
            base.SetButtons(images, orderedTypes);
        }
    }
}
