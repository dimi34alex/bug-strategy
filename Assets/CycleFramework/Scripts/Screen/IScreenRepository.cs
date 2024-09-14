using System;

namespace CycleFramework.Screen
{
    public interface IScreenRepository
    {
        public TScreen GetScreen<TScreen>() 
            where TScreen : UIScreen;

        public UIScreen GetScreen(Type screenType);
    }
}