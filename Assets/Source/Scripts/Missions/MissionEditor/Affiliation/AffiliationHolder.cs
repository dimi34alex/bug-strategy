using System;

namespace BugStrategy.Missions.MissionEditor.Affiliation
{
    public class AffiliationHolder
    {
        public AffiliationEnum PlayerAffiliation { get; private set; }

        public event Action OnAffiliationChanged;

        public AffiliationHolder(AffiliationEnum initialAffiliation = AffiliationEnum.Team1)
        {
            PlayerAffiliation = initialAffiliation;
        }
        
        public void SetAffiliation(AffiliationEnum newAffiliation)
        {
            if (PlayerAffiliation == newAffiliation)
                return;

            PlayerAffiliation = newAffiliation;
            OnAffiliationChanged?.Invoke();
        }
    }
}