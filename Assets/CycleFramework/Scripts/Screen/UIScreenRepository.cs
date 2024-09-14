using System;
using System.Collections.Generic;
using UnityEngine;

namespace CycleFramework.Screen
{
    [DisallowMultipleComponent]
    public class UIScreenRepository : MonoBehaviour, IScreenRepository
    {
        private Dictionary<Type, UIScreen> _screens;
    
        public void Initialize()
        {
            var screens = GetComponentsInChildren<UIScreen>(true);
            _screens = new Dictionary<Type, UIScreen>(screens.Length);
        
            foreach(var screen in screens)
                _screens.Add(screen.GetType(), screen);
        }

        public TScreen GetScreen<TScreen>() where TScreen : UIScreen
        {
            if (_screens.TryGetValue(typeof(TScreen), out var screen))
                return (TScreen)screen;

            return default;
        }
    
        public UIScreen GetScreen(Type screenType)
        {
            if (_screens.TryGetValue(screenType, out var screen))
                return screen;

            return default;
        }
    }
}
