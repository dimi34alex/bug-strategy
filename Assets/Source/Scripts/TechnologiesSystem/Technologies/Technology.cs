namespace BugStrategy.TechnologiesSystem.Technologies
{
    public abstract class Technology
    {
        public TechnologyState TechnologyState { get; private set; }
        public int Level { get; private set; }
        
        public bool Researched => TechnologyState == TechnologyState.Researched;
        
        public void Research()
        {
            TechnologyState = TechnologyState.Researched;
        }
    }

    public enum TechnologyState
    {
        None = 0,
        UnResearched = 1,
        IsResearch = 2,
        Researched = 4
    }
}