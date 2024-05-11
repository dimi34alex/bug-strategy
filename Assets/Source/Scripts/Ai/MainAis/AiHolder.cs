using System.Collections.Generic;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;
using Unit.Factory;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Ai
{
    public class AiHolder : MonoBehaviour
    {
        [SerializeField] private AffiliationEnum affiliationEnum;
        [SerializeField] private float timeBeforeAttackPlayerTownHall;
        [SerializeField] private List<ConstructionAiConfigBase> constructionAiConfigs;
        
        [Inject] private readonly UnitFactory _unitFactory;
        [Inject] private readonly IConstructionFactory _constructionFactory;
        [Inject] private readonly TeamsResourceGlobalStorage _teamsResourceGlobalStorage;
        
        private BeesGlobalAi _beesGlobalAi;
        
        private void Awake()
        {
            _beesGlobalAi = new BeesGlobalAi(affiliationEnum, _unitFactory, _constructionFactory, 
                timeBeforeAttackPlayerTownHall, _teamsResourceGlobalStorage, constructionAiConfigs);
        }

        private void Update()
        {
            _beesGlobalAi.HandleUpdate(Time.deltaTime);
        }
    }
}