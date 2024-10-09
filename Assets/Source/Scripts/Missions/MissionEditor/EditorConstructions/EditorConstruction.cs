using BugStrategy.Constructions;
using TMPro;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.EditorConstructions
{
    public class EditorConstruction : MonoBehaviour
    {
        public ConstructionID constructionID;

        private TMP_Text _affiliationView;
        
        public AffiliationEnum Affiliation { get; private set; }
        
        public void Initialize(AffiliationEnum affiliation)
        {
            Affiliation = affiliation;
            _affiliationView = GetComponentInChildren<TMP_Text>();
            _affiliationView.text = Affiliation.ToString();
        }
    }
}