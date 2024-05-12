using Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : UIScreen
{
    private static UserBuilder builder;
    private static GameObject UI_ActivScreen;
    private static UI_Gameplay UI_GameplayWindows;

    private UIRaceConfig _UIRaceConfig;

    private static GameObject UI_PrevActivScreen;

    private static UI_ERROR _uiError;

    private bool _isChooseState = true;

    private ConstructionBase _construction;

    private UnitBase _unitBase;

    private ConstructionUIController _constructionUIController;

    private TacticsUIView _tacticsUIView;
    private BuldingsUIView _buldingsUIView;

    private UnitInfoScreen _unitInfoScreen;

    private ConstructionInfoScreen _constructionInfoScreen;

    private ConstructionOperationUIView _constructionOperationUIView;
    private ConstructionProductsUIView _constructionProductsUIView;
    private ConstructionCreationProductUIView _сonstructionCreationProductUIView;

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

        _constructionInfoScreen = UIScreenRepository.GetScreen<ConstructionInfoScreen>();
        _constructionOperationUIView = UIScreenRepository.GetScreen<ConstructionOperationUIView>();
        _constructionProductsUIView = UIScreenRepository.GetScreen<ConstructionProductsUIView>();

        _buldingsUIView.ButtonClicked += OnBuldingInstance;
        _tacticsUIView.ButtonClicked += OnTacticsUse;

        _constructionOperationUIView.ButtonClicked += OnConstructionOperation;
        _constructionProductsUIView.ButtonClicked += OnConstructionProduct;

        _constructionUIController = UIScreenRepository.GetScreen<ConstructionUIController>();

        _сonstructionCreationProductUIView = UIScreenRepository.GetScreen<ConstructionCreationProductUIView>();

        _constructionInfoScreen.UpgradeClicked += OnConstructionUpgrated;

    }

    private void OnConstructionUpgrated()
    {
        //_construction.Upgrade))
    }

    private void OnConstructionProduct(ConstructionProduct constructionProduct)
    {

    }

    private void OnConstructionOperation(ConstructionOperationType constructionOperationType)
    {
        _isChooseState = false;
        _constructionUIController.SetWindow(_construction, _constructionInfoScreen,
        _constructionProductsUIView, _constructionOperationUIView, _isChooseState);


        switch (constructionOperationType)
        {
            case ConstructionOperationType.CreateUnit:
                BeeTownHall beeTownHall = _construction.Cast<BeeTownHall>();
   
                beeTownHall.RecruitUnit(UnitType.WorkerBee);
                Dictionary<UnitType, Sprite> dictin = new Dictionary<UnitType, Sprite>();

                dictin.Add(UnitType.WorkerBee, null);
                List<UnitType> units = new List<UnitType>();

                int a = beeTownHall.Recruiter.FindFreeStack();

                Debug.Log(a);
                units.Add(UnitType.WorkerBee);



                _сonstructionCreationProductUIView.SetButtons(dictin, units);


                break;

            case ConstructionOperationType.AllBeesGoToBase:
                break;
        }


    }

    private void OnBuldingInstance(ConstructionID constructionID)
    {
        builder.SpawnConstructionMovableModel(constructionID);
    }

    private void OnTacticsUse(UnitTacticsType unitTacticsType)
    {
        _isChooseState = false;
        switch (unitTacticsType)
        {
            case UnitTacticsType.Build:
               
                break;
            case UnitTacticsType.Repair:
               // _unitBase.AutoGiveOrder();
                break;
        }
    }

    private void CloseUnitInfoWindow()
    {
        _unitInfoScreen.gameObject.SetActive(false);
        _tacticsUIView.TurnOffButtons();
        _buldingsUIView.TurnOffButtons();
    }

    private void CloseConstructionInfoWindow()
    {
        _constructionInfoScreen.gameObject.SetActive(false);
        _constructionOperationUIView.TurnOffButtons();
        _constructionProductsUIView.TurnOffButtons();
    }

    public void SetWindow(UnitBase unitBase)
    {
        _unitBase = unitBase;
        UnitType unitType = unitBase.UnitType;

        CloseConstructionInfoWindow();
        _unitInfoScreen.gameObject.SetActive(true);

        try
        {
            UIUnitConfig unitUIConfig = _UIRaceConfig.UnitsUIConfigs[unitType];

            _unitInfoScreen.SetInfo(unitUIConfig.InfoSprite, unitBase.HealthStorage);
            _tacticsUIView.TurnOffButtons();
            _buldingsUIView.TurnOffButtons();

            if (_isChooseState)
              _tacticsUIView.SetButtons(unitUIConfig.UnitTacticsDictionary, unitUIConfig.UnitTactics
                    .Select(x => x.Key).ToList());
            else
                _buldingsUIView.SetButtons(unitUIConfig.UnitConstructionDictionary, unitUIConfig.UnitConstruction
                    .Select(x => x.Key).ToList());
   
        }
        catch(Exception exp)
        {
            throw new Exception("Настоятельно рекомендую проверить есть ли конфиг (UIUnitConfig и добавлен ли он " +
                "в UIRaceConfig)  " + exp.Message);
        }
    }

    public void SetWindow(ConstructionBase construction)
    {
        _construction = construction;
        CloseUnitInfoWindow();
        _constructionInfoScreen.gameObject.SetActive(true);
        _constructionUIController.ClearWindow();
        _constructionUIController.SetWindow(construction,_constructionInfoScreen,
        _constructionProductsUIView,_constructionOperationUIView, _isChooseState);
    }

    public void CloseisChooseState()
    {
        _isChooseState = true;
    }

    public void SetWindow(UIWindowType type)
    {
        GameObject screenBuffer = UI_ActivScreen;
        UI_ActivScreen.SetActive(false);

        switch (type)
        {
            case UIWindowType.Game:
                _isChooseState = true;
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
