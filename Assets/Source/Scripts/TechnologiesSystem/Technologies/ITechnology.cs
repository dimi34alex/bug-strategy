using System;
using BugStrategy.CustomTimer;

namespace BugStrategy.TechnologiesSystem.Technologies
{
    public interface ITechnology
    {
        public string TechName { get; }
        public string Description { get; }
        public TechnologyId Id { get; }
        public TechnologyState State { get; }
        public IReadOnlyTimer ResearchTimer { get; }
        public int Level { get; }

        public bool Researched => State == TechnologyState.Researched;

        public event Action OnDataChanged;

        public void HandleUpdate(float deltaTime);

        public void Research();
        
        Cost GetCost();
    }
}