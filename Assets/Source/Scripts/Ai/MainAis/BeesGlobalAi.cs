using System.Collections.Generic;
using BugStrategy.Ai.ConstructionsAis;
using BugStrategy.Constructions;
using BugStrategy.Constructions.Factory;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit;
using BugStrategy.Unit.Factory;

namespace BugStrategy.Ai.MainAis
{
    public class BeesGlobalAi : IGlobalAi
    {
        private readonly AffiliationEnum _affiliation;
        private readonly UnitsAiRepository _unitsAiRepository;
        private readonly CombatAi _combatAi;
        private readonly ResourceCollectorAi _resourceCollectorAi;
        private readonly ConstructionsAi _constructionsAi;
        private readonly IConstructionFactory _constructionFactory;

        public BeesGlobalAi(AffiliationEnum affiliation, UnitFactory unitFactory, IConstructionFactory constructionFactory, 
            float timeBeforeAttackPlayerTownHall, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, 
            IEnumerable<ConstructionAiConfigBase> constructionsConfigs)
        {
            _affiliation = affiliation;
            _unitsAiRepository = new UnitsAiRepository(_affiliation, unitFactory);
            _combatAi = new CombatAi(_affiliation, timeBeforeAttackPlayerTownHall, _unitsAiRepository);
            _resourceCollectorAi = new ResourceCollectorAi(_unitsAiRepository);
            _constructionsAi = new ConstructionsAi(teamsResourcesGlobalStorage, _unitsAiRepository, constructionsConfigs);
            _constructionFactory = constructionFactory;
            
            _constructionFactory.Created += TryAddConstruction;
        }

        public void HandleUpdate(float deltaTime)
        {
            _combatAi.HandleUpdate(deltaTime);
            _resourceCollectorAi.HandleUpdate(deltaTime);
            _constructionsAi.HandleUpdate(deltaTime);
        }

        private void TryAddConstruction(ConstructionBase construction)
        {
            if(construction.Affiliation != _affiliation)
                return;

            _constructionsAi.TryAddConstruction(construction);
        }
    }
}