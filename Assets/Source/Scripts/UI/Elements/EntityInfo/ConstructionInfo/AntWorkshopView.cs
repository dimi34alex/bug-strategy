using System;
using System.Collections.Generic;
using BugStrategy.Constructions;
using BugStrategy.Unit;
using TMPro;
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
            [SerializeField] private Image rang1;
            [SerializeField] private TMP_Text rang1Count;
            [SerializeField] private Image rang2;
            [SerializeField] private TMP_Text rang2Count;
            [SerializeField] private Image rang3;
            [SerializeField] private TMP_Text rang3Count;

            public void TurnOff()
            {
                rang1.gameObject.SetActive(false);
                rang1Count.gameObject.SetActive(false);
                
                rang2.gameObject.SetActive(false);
                rang2Count.gameObject.SetActive(false);
                
                rang3.gameObject.SetActive(false);
                rang3Count.gameObject.SetActive(false);
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
                            rang1.gameObject.SetActive(true);
                            rang1Count.gameObject.SetActive(true);
                            rang1Count.text = data[i].ToString();
                            break;
                        case 1:
                            rang2.gameObject.SetActive(true);
                            rang2Count.gameObject.SetActive(true);
                            rang2Count.text = data[i].ToString();
                            break;
                        case 2:
                            rang3.gameObject.SetActive(true);
                            rang3Count.gameObject.SetActive(true);
                            rang3Count.text = data[i].ToString();
                            break;
                    }
                }
            }
        }
    }
}