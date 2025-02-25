using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem.Technologies;
using UnityEngine;

namespace BugStrategy.TechnologiesSystem
{
    public class TechnologyModule
    {
        private readonly TechnologiesTeamRepository _technologiesTeamRepository;
        private readonly TeamsResourcesGlobalStorage _resourcesGlobalStorage;
        
        public TechnologyModule(TechnologiesFactory factory, TeamsResourcesGlobalStorage resourcesGlobalStorage)
        {
            _technologiesTeamRepository = new TechnologiesTeamRepository(factory);
            _resourcesGlobalStorage = resourcesGlobalStorage;
        }
        
        public void HandleUpdate(float deltaTime) 
            => _technologiesTeamRepository.HandleUpdate(deltaTime);

        public ITechnology GetTechnology(AffiliationEnum affiliation, TechnologyId id) 
            => _technologiesTeamRepository.GetTechnology(affiliation, id);

        public T GetTechnology<T>(AffiliationEnum affiliation, TechnologyId id) where T: ITechnology 
            => _technologiesTeamRepository.GetTechnology<T>(affiliation, id);

        public void Unlock(AffiliationEnum affiliation, TechnologyId id) 
            => _technologiesTeamRepository.Unlock(affiliation, id);

        public bool CanBuy(AffiliationEnum affiliation, TechnologyId id)
        {
            var technology = _technologiesTeamRepository.GetTechnology(affiliation, id);
            if (technology.State != TechnologyState.UnResearched)
            {
                Debug.LogWarning($"You try research technology that you cant research: {affiliation} {id} {technology.State}");
                return false;
            }
            
            var cost = technology.GetCost();
            return _resourcesGlobalStorage.CanBuy(affiliation, cost);
        }

        public void TryResearchTechnology(AffiliationEnum affiliation, TechnologyId id)
        {
            var technology = _technologiesTeamRepository.GetTechnology(affiliation, id);
            if (technology.State != TechnologyState.UnResearched)
            {
                Debug.LogWarning($"You try research technology that you cant research: {affiliation} {id} {technology.State}");
                return;
            }

            if (!technology.Unlocked)
            {
                Debug.LogWarning($"You try research technology, that are locked: [{id}] [{GetType()}]");
                return;
            }
            
            var cost = technology.GetCost();
            if (_resourcesGlobalStorage.CanBuy(affiliation, cost))
            {
                technology.Research();
                _resourcesGlobalStorage.SpendResources(affiliation, cost);
            }
            else
            {
                Debug.LogWarning($"You try research technology that you cant buy: {affiliation} {id} " +
                                 $"\n Cost: \n {cost}");
                return;
            }
        }
    }
}