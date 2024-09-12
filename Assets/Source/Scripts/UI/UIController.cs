using Source.Scripts.UI.EntityInfo.ConstructionInfo;
using Source.Scripts.UI.EntityInfo.UnitInfo;
using Source.Scripts.UI.UI_WindowsBlocksScripts;
using Source.Scripts.UI.UI_WindowsBlocksScripts.UI_Gameplay;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class UIController : UIScreen
    {
        private static UserBuilder builder;
        private static GameObject UI_ActivScreen;
        private static UI_Gameplay UI_GameplayWindows;

        private UIUnitsConfig _uiUnitsConfig;

        private static GameObject UI_PrevActivScreen;

        private static UI_ERROR _uiError;
    
        private UnitInfoScreen _unitInfoScreen;

        private ConstructionInfoScreen _constructionInfoScreen;
    
        private void Start()
        {
            builder = GameObject.Find("Builder").GetComponent<UserBuilder>();
            if(builder == null)
                Debug.LogError("Builder is null");
        
            UI_GameplayWindows = UIScreenRepository.GetScreen<UI_Gameplay>();
            _uiUnitsConfig = ConfigsRepository.FindConfig<UIUnitsConfig>();
            //определяем, какое окно у нас активно при запуске.
            if (UIScreenRepository.GetScreen<UI_Gameplay>().isActiveAndEnabled)
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_Gameplay>().gameObject;
            else
            if (UIScreenRepository.GetScreen<UI_Buildings>().isActiveAndEnabled)
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_Buildings>().gameObject;
            else
            if (UIScreenRepository.GetScreen<UI_Tactics>().isActiveAndEnabled)
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_Tactics>().gameObject;
            else
            if (UIScreenRepository.GetScreen<UI_GameplayMenu>().isActiveAndEnabled)
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_GameplayMenu>().gameObject;
            else
            if (UIScreenRepository.GetScreen<UI_Settings>().isActiveAndEnabled)
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_Settings>().gameObject;
            else
            if (UIScreenRepository.GetScreen<UI_Win>().isActiveAndEnabled)
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_Win>().gameObject;
            else
            if (UIScreenRepository.GetScreen<UI_Lose>().isActiveAndEnabled)
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_Lose>().gameObject;
            else
            if (UIScreenRepository.GetScreen<UI_MainMenu>().isActiveAndEnabled)
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_MainMenu>().gameObject;
            else
            if (UIScreenRepository.GetScreen<UI_Saves>().isActiveAndEnabled)
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_Saves>().gameObject;

            UI_PrevActivScreen = UI_ActivScreen;

            _uiError =  UIScreenRepository.GetScreen<UI_ERROR>();
        
            _unitInfoScreen = UIScreenRepository.GetScreen<UnitInfoScreen>();

            _constructionInfoScreen = UIScreenRepository.GetScreen<ConstructionInfoScreen>();
        }

        private void OnBuldingInstance(ConstructionID constructionID) 
            => builder.SpawnConstructionMovableModel(constructionID);
    
        private void CloseUnitInfoWindow() 
            => _unitInfoScreen.Hide();

        private void CloseConstructionInfoWindow() 
            => _constructionInfoScreen.Hide();

        public void SetWindow(UnitBase unitBase)
        {
            CloseUnitInfoWindow();
            _unitInfoScreen.Show();
            _unitInfoScreen.SetUnit(unitBase);
        }

        public void SetWindow(ConstructionBase construction)
        {
            CloseUnitInfoWindow();
            _constructionInfoScreen.Show();
            _constructionInfoScreen.SetConstruction(construction);
        }

        public void SetWindow(UIWindowType type)
        {
            GameObject screenBuffer = UI_ActivScreen;
            UI_ActivScreen.SetActive(false);

            switch (type)
            {
                case UIWindowType.Game:
                    UnitSelection.Instance.DeselectAll();
                    CloseUnitInfoWindow();
                    CloseConstructionInfoWindow();
                    UI_ActivScreen = UIScreenRepository.GetScreen<UI_Gameplay>().gameObject; 
                    break;
                case UIWindowType.GameMain:
                    UI_GameplayWindows.SetGameplayWindow(UIWindowType.GameMain, null); 
                    break;
                case UIWindowType.GameplayMenu:
                    UI_ActivScreen = UIScreenRepository.GetScreen<UI_GameplayMenu>().gameObject; 
                    break;
                case UIWindowType.Settings:
                    UI_ActivScreen = UIScreenRepository.GetScreen<UI_Settings>().gameObject; 
                    break;
                case UIWindowType.GameWin:
                    UI_ActivScreen = UIScreenRepository.GetScreen<UI_Win>().gameObject; 
                    break;
                case UIWindowType.GameLose:
                    UI_ActivScreen = UIScreenRepository.GetScreen<UI_Lose>().gameObject; 
                    break;
                case UIWindowType.Menu:
                    UI_ActivScreen = UIScreenRepository.GetScreen<UI_MainMenu>().gameObject;
                    break;
                case UIWindowType.Saves:
                    UI_ActivScreen = UIScreenRepository.GetScreen<UI_Saves>().gameObject; 
                    break;
                case UIWindowType.Back:
                    UI_ActivScreen = UI_PrevActivScreen; 
                    break;
            }

            UI_PrevActivScreen = screenBuffer;
            UI_ActivScreen.SetActive(true);
        }

        public static void ErrorCall(string error)
        {
            _uiError.ErrorCall(error);
        }
    
        public static void Quite()
        {
            Application.Quit();
        }
    }
}
