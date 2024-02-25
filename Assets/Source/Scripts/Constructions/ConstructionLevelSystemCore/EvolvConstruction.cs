using UnityEngine;

namespace Constructions.LevelSystemCore
{
    public abstract class EvolvConstruction : ConstructionBase
    {
        protected IConstructionLevelSystem levelSystem;
    
        public void LevelUp()
        {
            if (levelSystem.LevelCapCheck())
            {
                UI_Controller._ErrorCall("Max building level");
                return;
            }
        
            if (!levelSystem.PriceCheck())
            {
                UI_Controller._ErrorCall("Need more resources");
                return;
            }
        
            levelSystem.TryLevelUp();
        
            Debug.Log("Construction LVL = " + levelSystem.CurrentLevelNum);
        }
    }
}
