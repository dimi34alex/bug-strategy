using System;
using UnityEngine;
using Zenject;

namespace BugStrategy.UI
{
    public class UiControllerInstaller : MonoInstaller
    {
        [SerializeField] private UIController uiController;
        
        public override void InstallBindings()
        {
            if (uiController == null)
            {
                uiController = FindObjectOfType<UIController>(true);

                Debug.LogWarning("You forgot serialize ui controller");
                if (uiController == null)
                    throw new NullReferenceException("Cant find ui controller");
            }
            
            Container.BindInterfacesAndSelfTo<UIController>().FromInstance(uiController).AsSingle();
        }
    }
}