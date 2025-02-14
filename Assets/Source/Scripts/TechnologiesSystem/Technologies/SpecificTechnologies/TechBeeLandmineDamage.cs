using BugStrategy.TechnologiesSystem.Technologies.Configs;

namespace BugStrategy.TechnologiesSystem.Technologies
{
    public class TechBeeLandmineDamage : Technology
    {
        private readonly TechBeeLandmineDamageConfig _config;

        public TechBeeLandmineDamage(TechBeeLandmineDamageConfig config)
        {
            _config = config;
        }

        public float GetDamageScale()
        {
            if (Researched)
                return _config.DamageScale;

            return 1;
        }
    }
}