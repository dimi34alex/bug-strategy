using BugStrategy.TechnologiesSystem.Technologies.Configs;

namespace BugStrategy.TechnologiesSystem.Technologies
{
    public class TechHoneyCatapult : Technology<TechnologyConfig>
    {
        public TechHoneyCatapult(TechnologyConfig config) 
            : base(config) { }
    }
}