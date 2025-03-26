using System;
using BugStrategy.Missions.MissionEditor.Grids;
using UnityEngine;
using Zenject;

namespace BugStrategy.Grids.GridsProviding
{
    public class GridsProviderInstaller : MonoInstaller
    {
        [SerializeField] private SceneType sceneType;

        public override void InstallBindings()
        {
            switch (sceneType)
            {
                case SceneType.Gameplay:
                    Container.BindInterfacesAndSelfTo<GameplayGridsProvider>().FromNew().AsSingle();
                    break;
                case SceneType.Editor:
                    Container.BindInterfacesAndSelfTo<MissionEditorGridsProvider>().FromNew().AsSingle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private enum SceneType
        {
            Gameplay = 0,
            Editor = 1
        }
    }
}