using System.Collections.Generic;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Ai
{
    public class AiInstaller : MonoInstaller
    {
        //TODO: move it in config
        [SerializeField] private float timeBeforeAttackPlayerTownHall;
        [SerializeField] private List<ConstructionAiConfigBase> constructionAiConfigs;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AisProvider>().FromNew().AsSingle()
                .WithArguments(timeBeforeAttackPlayerTownHall, constructionAiConfigs).NonLazy();
        }
    }
}