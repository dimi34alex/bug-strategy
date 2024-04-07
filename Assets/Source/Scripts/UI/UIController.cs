using System;
using System.Linq;
using UnityEngine;

public class UIController : UIScreen
{
    private static UserBuilder builder;
    private static GameObject UI_ActivScreen;
    private static UI_Gameplay UI_GameplayWindows;

    private static UIRaceConfig _UIRaceConfig;

    private static GameObject UI_PrevActivScreen;

    private static UI_ERROR _uiError;

    private TacticsUIView _tacticsUIView;
    private BuldingsUIView _buldingsUIView;
    private UnitInfoScreen _unitInfoScreen;

    private void Start()
    {
        builder = GameObject.Find("Builder").GetComponent<UserBuilder>();
        if(builder == null)
            Debug.LogError("Builder is null");
        
        UI_GameplayWindows = UIScreenRepository.GetScreen<UI_Gameplay>();
        _UIRaceConfig = ConfigsRepository.FindConfig<UIRaceConfig>();
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
        _tacticsUIView = UIScreenRepository.GetScreen<TacticsUIView>();
        _buldingsUIView = UIScreenRepository.GetScreen<BuldingsUIView>();
        _unitInfoScreen = UIScreenRepository.GetScreen<UnitInfoScreen>();

    }

    public void SetWindow(UnitBase unitBase)
    {
        UnitType unitType = unitBase.UnitType;

        try
        {
            UIUnitConfig unitUIConfig = _UIRaceConfig.UnitsUIConfigs[unitType];

            _unitInfoScreen.SetInfoUnit(unitUIConfig.InfoSprite, unitBase.HealthStorage);

            _tacticsUIView.TurnOffButtons();
            _buldingsUIView.TurnOffButtons();

            if (unitUIConfig == null || unitUIConfig.UnitConstruction.Count == 0)
            {
                _tacticsUIView.SetButtons(unitUIConfig.UnitTacticsDictionary, unitUIConfig.UnitTactics
                    .Select(x => x.Key).ToList());
            }
            else
            {
                _buldingsUIView.SetButtons(unitUIConfig.UnitConstructionDictionary, unitUIConfig.UnitConstruction
                    .Select(x => x.Key).ToList());
            }
        }
        catch(Exception exp)
        {
            throw new Exception("Настоятельно рекомендую проверить есть ли конфиг (UIUnitConfig и добавлен ли он " +
                "в UIRaceConfig)  " + exp.Message);
        }
    }

    public void SetWindow(UIWindowType type)
    {
        GameObject screenBuffer = UI_ActivScreen;
        UI_ActivScreen.SetActive(false);

        switch (type)
        {
            case UIWindowType.Game:
                UI_ActivScreen = UIScreenRepository.GetScreen<UI_Gameplay>().gameObject; 
                break;
            case UIWindowType.GameMain:
                UI_GameplayWindows.SetGameplayWindow(UIWindowType.GameMain, null); 
                break;
            case UIWindowType.Building:
                UI_GameplayWindows.SetGameplayWindow(UIWindowType.Building, null);
                break;
            case UIWindowType.Tactics:
                UI_GameplayWindows.SetGameplayWindow(UIWindowType.Tactics, null); 
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

    public void SetBuilding(ConstructionBase newConstruction)
    {
        UI_GameplayWindows.SetGameplayWindow(UIWindowType.ConstructionMenu, newConstruction);
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
