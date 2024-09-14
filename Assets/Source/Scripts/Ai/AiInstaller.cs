using System.Collections.Generic;
using BugStrategy.Ai.ConstructionsAis;
using UnityEngine;
using Zenject;

namespace BugStrategy.Ai
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