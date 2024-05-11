using System.Collections.Generic;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;
using Unit.Factory;

namespace Source.Scripts.Ai
{
    public class BeesGlobalAi
    {
        private readonly AffiliationEnum _affiliation;
        private readonly UnitsAiRepository _unitsAiRepository;
        private readonly CombatAi _combatAi;
        private readonly ResourceCollectorAi _resourceCollectorAi;
        private readonly ConstructionsAi _constructionsAi;
        private readonly IConstructionFactory _constructionFactory;

        public BeesGlobalAi(AffiliationEnum affiliation, UnitFactory unitFactory, IConstructionFactory constructionFactory, 
            float timeBeforeAttackPlayerTownHall, TeamsResourceGlobalStorage teamsResourceGlobalStorage, 
            IEnumerable<ConstructionAiConfigBase> constructionsConfigs)
        {
            _affiliation = affiliation;
            _unitsAiRepository = new UnitsAiRepository(_affiliation, unitFactory);
            _combatAi = new CombatAi(_affiliation, timeBeforeAttackPlayerTownHall, _unitsAiRepository);
            _resourceCollectorAi = new ResourceCollectorAi(_unitsAiRepository);
            _constructionsAi = new ConstructionsAi(teamsResourceGlobalStorage, _unitsAiRepository, constructionsConfigs);
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