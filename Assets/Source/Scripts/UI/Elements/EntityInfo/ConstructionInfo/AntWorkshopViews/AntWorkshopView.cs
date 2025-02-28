using System;
using System.Collections.Generic;
using BugStrategy.Constructions;
using BugStrategy.Unit;
using BugStrategy.Unit.Ants.GetToolOrdere;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo.AntWorkshopViews
{
    public class AntWorkshopView : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Composite standardAntProfessionsView;
        [SerializeField] private Composite bigAntProfessionsView;
        [SerializeField] private Composite flyingAntProfessionsView;

        [SerializeField] private CreateLine standardAntTools;
        [SerializeField] private CreateLine bigAntTools;
        [SerializeField] private CreateLine flyingAntTools;

        [Inject] private UIAntsToolsConfig _uiAntsToolsConfig;
        [Inject] private PlayerAntsGetToolOrderer _playerAntsGetToolOrderer;
        
        private AntWorkshopBase _workshopConstruction;
        
        public event Action BackButtonClicked;

        private void Awake()
        {
            backButton.onClick.AddListener(() => BackButtonClicked?.Invoke());

            standardAntTools.OnPressed += CreateTool;
            bigAntTools.OnPressed += CreateTool;
            flyingAntTools.OnPressed += CreateTool;
            
            standardAntProfessionsView.Initialize(UnitType.AntStandard);
            bigAntProfessionsView.Initialize(UnitType.AntBig);
            flyingAntProfessionsView.Initialize(UnitType.AntFlying);
            
            standardAntProfessionsView.OnPressed += GiveOrder;
            bigAntProfessionsView.OnPressed += GiveOrder;
            flyingAntProfessionsView.OnPressed += GiveOrder;
            
            standardAntTools.Initialize(UnitType.AntStandard);
            bigAntTools.Initialize(UnitType.AntBig);
            flyingAntTools.Initialize(UnitType.AntFlying);
        }

        public void Show(bool showBackButton, AntWorkshopBase workshopConstruction)
        {
            backButton.gameObject.SetActive(showBackButton);
            
            _workshopConstruction = workshopConstruction;
            _workshopConstruction.WorkshopCore.OnChange += UpdateView;

            gameObject.SetActive(true);
            UpdateView();
        }

        public void Hide()
        {
            if (_workshopConstruction != null) 
                _workshopConstruction.WorkshopCore.OnChange -= UpdateView;

            gameObject.SetActive(false);
        }

        private void GiveOrder(UnitType unitType, int rang) 
            => _playerAntsGetToolOrderer.GiveOrder(_workshopConstruction, unitType, _workshopConstruction.WorkshopCore.ProfessionType, rang);

        private void CreateTool(UnitType unitType, int toolRang) 
            => _workshopConstruction.WorkshopCore.CreateTool(unitType, toolRang);

        private void UpdateView()
        {
            UpdateView(UnitType.AntStandard, standardAntProfessionsView, standardAntTools);
            UpdateView(UnitType.AntBig, bigAntProfessionsView, bigAntTools);
            UpdateView(UnitType.AntFlying, flyingAntProfessionsView, flyingAntTools);
        }

        private void UpdateView(UnitType unitType, Composite composite, CreateLine createLine)
        {
            var data = _workshopConstruction.WorkshopCore.GetToolData(unitType);
            
            composite.TurnOff();
            composite.Show(data, _workshopConstruction.WorkshopCore.RangAccess);
            createLine.Show(_uiAntsToolsConfig.GetIcons(unitType, _workshopConstruction.WorkshopCore.ProfessionType),
                _workshopConstruction.WorkshopCore.GetMaxAvailableRang(unitType));
        }
        
        [Serializable]
        private class Composite
        {
            [SerializeField] private ButtonWithImageAndText rang1;
            [SerializeField] private ButtonWithImageAndText rang2;
            [SerializeField] private ButtonWithImageAndText rang3;

            private UnitType _unitType;
            
            /// <summary>
            /// return UnitType and tool rang
            /// </summary>
            public event Action<UnitType, int> OnPressed;
            
            public void Initialize(UnitType unitType)
            {
                _unitType = unitType;
                rang1.Button.onClick.AddListener(() => OnPressed?.Invoke(_unitType, 1));
                rang2.Button.onClick.AddListener(() => OnPressed?.Invoke(_unitType, 2));
                rang3.Button.onClick.AddListener(() => OnPressed?.Invoke(_unitType, 3));
            }
            
            public void TurnOff()
            {
                rang1.Hide();
                rang2.Hide();
                rang3.Hide();
            }

            public void Show(IReadOnlyDictionary<int,int> data, int maxRang)
            {
                for (int i = 0; i < maxRang; i++)
                {
                    if (!data.ContainsKey(i))
                        continue;

                    switch (i)
                    {
                        case 0:
                            rang1.Show();
                            rang1.SetText(data[i].ToString());
                            break;
                        case 1:
                            rang2.Show();
                            rang2.SetText(data[i].ToString());
                            break;
                        case 2:
                            rang3.Show();
                            rang3.SetText(data[i].ToString());
                            break;
                    }
                }
            }
        }
    }
}