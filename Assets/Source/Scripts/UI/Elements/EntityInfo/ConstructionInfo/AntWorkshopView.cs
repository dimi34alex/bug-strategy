using System;
using System.Collections.Generic;
using BugStrategy.Constructions;
using BugStrategy.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo
{
    public class AntWorkshopView : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Composite standardAntProfessionsView;
        [SerializeField] private Composite bigAntProfessionsView;
        [SerializeField] private Composite flyingAntProfessionsView;

        private AntWorkshopBase _workshopConstruction;
        
        public event Action BackButtonClicked;

        private void Awake()
        {
            backButton.onClick.AddListener(() => BackButtonClicked?.Invoke());
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
            gameObject.SetActive(false);
        }

        private void UpdateView()
        {
            UpdateView(UnitType.AntStandard, standardAntProfessionsView);
            UpdateView(UnitType.AntBig, bigAntProfessionsView);
            UpdateView(UnitType.AntFlying, flyingAntProfessionsView);
        }

        private void UpdateView(UnitType unitType, Composite composite)
        {
            var data = _workshopConstruction.WorkshopCore.GetToolData(unitType);
            
            composite.TurnOff();
            composite.Show(data, _workshopConstruction.WorkshopCore.RangAccess);
        }
        
        [Serializable]
        private class Composite
        {
            [SerializeField] private ImageAndTextHolder rang1;
            [SerializeField] private ImageAndTextHolder rang2;
            [SerializeField] private ImageAndTextHolder rang3;

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