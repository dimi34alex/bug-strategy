using BugStrategy.Constructions;
using TMPro;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.EditorConstructions
{
    public class EditorConstruction : MonoBehaviour
    {
        [SerializeField] private TMP_Text affiliationView;
        
        public ConstructionID constructionID;
        public AffiliationEnum Affiliation { get; private set; }
        
        public void Initialize(AffiliationEnum affiliation)
        {
            Affiliation = affiliation;
            affiliationView.text = Affiliation.ToString();
        }
    }
}