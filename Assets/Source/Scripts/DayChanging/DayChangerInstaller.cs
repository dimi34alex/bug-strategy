using UnityEngine;
using Zenject;

namespace BugStrategy.DayChanging
{
    public class DayChangerInstaller : MonoInstaller
    {
        [SerializeField] private DayChanger dayChanger;
            
        public override void InstallBindings()
        {
            Container.BindInstance(dayChanger).AsSingle();
        }
    }
}