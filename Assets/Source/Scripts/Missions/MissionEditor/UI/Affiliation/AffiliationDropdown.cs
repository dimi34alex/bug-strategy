using System.Collections.Generic;
using System.Linq;
using BugStrategy.Libs;
using BugStrategy.Missions.MissionEditor.Affiliation;
using CycleFramework.Extensions;
using TMPro;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.MissionEditor.UI.Affiliation
{
    public class AffiliationDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;

        [Inject] private AffiliationHolder _affiliationHolder;
        
        private IReadOnlyList<AffiliationEnum> _affiliations;
        
        private void Awake()
        {
            Initialize();
            SetView();
        }

        private void Initialize()
        {
            dropdown.ClearOptions();

            _affiliations = EnumValuesTool.GetValues<AffiliationEnum>().ToList();
            var newOptions = new List<TMP_Dropdown.OptionData>(_affiliations.Count());
            foreach (var affiliation in _affiliations)
                newOptions.Add(new TMP_Dropdown.OptionData(affiliation.ToString()));

            dropdown.AddOptions(newOptions);
            
            dropdown.onValueChanged.AddListener(SetAffiliation);
        }
        
        private void SetView()
        {
            var index = _affiliations.IndexOf(a => a == _affiliationHolder.PlayerAffiliation);
            dropdown.SetValueWithoutNotify(index);
        }
        
        private void SetAffiliation(int id)
        {
            var affiliation = _affiliations[id];
            _affiliationHolder.SetAffiliation(affiliation);
        }
    }
}