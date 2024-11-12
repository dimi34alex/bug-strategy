using System;
using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo.AntWorkshopViews
{
    public class CreateLine : MonoBehaviour
    {
        [SerializeField] private AntWorkshopButtonsPanel buttonsPanel;

        private UnitType _unitType;
        
        public event Action<UnitType, int> OnPressed;

        public void Initialize(UnitType unitType)
        {
            _unitType = unitType;
            buttonsPanel.ButtonClicked += toolRang => OnPressed?.Invoke(_unitType, toolRang);
        }

        public void Show(IReadOnlyDictionary<int, Sprite> images, int maxRang)
        {
            var orders = new List<int>(maxRang);
            for (int i = 0; i < maxRang; i++) 
                orders.Add(i);
            
            buttonsPanel.SetButtons(false, images, orders);
        }
    }
}