using System;

namespace BugStrategy.CycleFramework.Scripts.Screen
{
    public interface IScreenRepository
    {
        public TScreen GetScreen<TScreen>() 
            where TScreen : UIScreen;

        public UIScreen GetScreen(Type screenType);
    }
}