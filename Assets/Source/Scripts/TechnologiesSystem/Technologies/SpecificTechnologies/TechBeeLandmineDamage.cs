using BugStrategy.TechnologiesSystem.Technologies.Configs;

namespace BugStrategy.TechnologiesSystem.Technologies
{
    public class TechBeeLandmineDamage : Technology<TechBeeLandmineDamageConfig>
    {
        public TechBeeLandmineDamage(TechBeeLandmineDamageConfig config) 
            : base(config) { }

        public float GetDamageScale()
        {
            if (Researched)
                return _config.DamageScale;

            return 1;
        }
        
        public float GetDamageRadiusScale()
        {
            if (Researched)
                return _config.DamageRadiusScale;

            return 1;
        }
    }
}