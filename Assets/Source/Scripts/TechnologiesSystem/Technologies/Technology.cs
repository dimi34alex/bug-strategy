using System;
using BugStrategy.CustomTimer;
using BugStrategy.TechnologiesSystem.Technologies.Configs;
using UnityEngine;

namespace BugStrategy.TechnologiesSystem.Technologies
{
    public abstract class Technology<T> : ITechnology
        where T : TechnologyConfig
    {
        public string TechName => _config.TechName;
        public string Description => _config.Description;
        public string UnlockRequirements => _config.UnlockRequirements;
        public bool Unlocked { get; private set; }
        public TechnologyId Id => _config.Id;
        public TechnologyState State { get; private set; }
        public int Level { get; private set; }
        
        public bool Researched => State == TechnologyState.Researched;
        public IReadOnlyTimer ResearchTimer => _researchTimer;

        protected readonly T _config;
        private readonly Timer _researchTimer;
        
        public event Action OnDataChanged;

        protected Technology(T config)
        {
            _config = config;
            Unlocked = _config.IsUnlockedByDefault;
            _researchTimer = new Timer(_config.ResearchTime, _config.ResearchTime);
            _researchTimer.OnTimerEnd += EndResearch;
        }
        
        public void HandleUpdate(float deltaTime)
        {
            if (!_researchTimer.IsActive)
                return;

            _researchTimer.Tick(deltaTime);
            OnDataChanged?.Invoke();
        }

        public void Unlock()
        {
            if (Unlocked)
            {
                Debug.LogWarning($"You try unlock technology, that already unlocked: [{Id}] [{GetType()}]");
                return;
            }

            Unlocked = true;
            OnDataChanged?.Invoke();
        }

        public void Research()
        {
            if(!Unlocked)
            {
                Debug.LogWarning($"You try research technology, that are locked: [{Id}] [{GetType()}]");
                return;
            }

            if (State != TechnologyState.UnResearched)
            {
                Debug.LogWarning($"You try research technology, that you cant research: [{Id}] [{GetType()}] [{State}]");
                return;
            }

            State = TechnologyState.ResearchProcess;
            _researchTimer.Reset();
            
            OnDataChanged?.Invoke();
        }

        public Cost GetCost() 
            => _config.TakeCost();

        private void EndResearch()
        {
            State = TechnologyState.Researched;
            OnDataChanged?.Invoke();
        }
    }
}