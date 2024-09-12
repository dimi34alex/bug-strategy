using Constructions;

namespace Source.Scripts.UI.UI_WindowsBlocksScripts.UI_Gameplay
{
    public class UI_BeeHouseMenu : UI_EvolveConstructionScreenBase<BeeHouse>
    {
        public void _CallMenu(ConstructionBase beeHouse)
        {
            _construction = beeHouse.Cast<BeeHouse>();
        }
    }
}
