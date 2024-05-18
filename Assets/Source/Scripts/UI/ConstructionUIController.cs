using System.Linq;

public class ConstructionUIController : UIScreen
{
    private UIConstructionsConfig _UIConstructionsConfig;

    private void Awake()
    {
        _UIConstructionsConfig = ConfigsRepository.FindConfig<UIConstructionsConfig>();
    }


    public void ClearWindow()
    {

    }

    public void SetPanel(ConstructionBase construction, ConstructionCreationProductUIView сonstructionCreationProductUIView)
    {
        UIConstructionConfig constructionUIConfig = _UIConstructionsConfig.ConstructionsUIConfigs[construction.ConstructionID];

        сonstructionCreationProductUIView.CloseAll();
    }


    public void SetWindow(ConstructionBase construction, ConstructionInfoScreen constructionInfoScreen,
        ConstructionProductsUIView constructionProductsUIView, ConstructionOperationUIView constructionOperationUIView,
        bool isChooseState, ConstructionCreationProductUIView сonstructionCreationProductUIView)
    {
        UIConstructionConfig constructionUIConfig = _UIConstructionsConfig.ConstructionsUIConfigs[construction.ConstructionID];

        сonstructionCreationProductUIView.CloseAll();

        constructionInfoScreen.SetInfo(constructionUIConfig.InfoSprite, construction.HealthStorage);

        constructionProductsUIView.TurnOffButtons();
        constructionOperationUIView.TurnOffButtons();

        if (constructionUIConfig.ConstructionProducts == null ||  constructionUIConfig.ConstructionProducts.Count == 0 )
            isChooseState = true;

        if (isChooseState)
        {
            constructionOperationUIView.SetButtons(constructionUIConfig.ConstructionOperationsDictionary, constructionUIConfig.ConstructionOperations
                    .Select(x => x.Key).ToList());
        }
        else
        {
            constructionProductsUIView.SetButtons(constructionUIConfig.ConstructionProductsDictionary, constructionUIConfig.ConstructionProducts
                  .Select(x => x.Key).ToList());
        }

        if ((constructionUIConfig.ConstructionProducts != null &&  constructionUIConfig.ConstructionProducts.Count > 0)||
            (constructionUIConfig.ConstructionOperations != null && constructionUIConfig.ConstructionOperations.Count > 0))
        {

            сonstructionCreationProductUIView.ActivatePanel();
        }

            сonstructionCreationProductUIView.ActivateBar();


        /*  _UI_TownHallMenu = UIScreenRepository.GetScreen<UI_BeeTownHallMenu>().gameObject;
          _UI_BarracksMenu = UIScreenRepository.GetScreen<UI_BeeBarracksMenu>().gameObject;
          _UI_BeeHouseMenu = UIScreenRepository.GetScreen<UI_BeeHouseMenu>().gameObject;
          _UI_BeesWaxProduceConstructionMenu = UIScreenRepository.GetScreen<UI_BeesWaxProduceConstructionMenu>().gameObject;

  */

        /*    _gameplayWindowsSetActions.Add("UI_TownHallMenu", () => {
                _UI_TownHallMenu.SetActive(true);
                _UI_TownHallMenu.GetComponent<UI_BeeTownHallMenu>().CallMenu(_construction);
            });
            _gameplayWindowsSetActions.Add("UI_BarracksMenu", () => {
                _UI_BarracksMenu.SetActive(true);
                _UI_BarracksMenu.GetComponent<UI_BeeBarracksMenu>()._CallMenu(_construction);
            });
            _gameplayWindowsSetActions.Add("UI_BeeHouseMenu", () => {
                _UI_BeeHouseMenu.SetActive(true);
                _UI_BeeHouseMenu.GetComponent<UI_BeeHouseMenu>()._CallMenu(_construction);
            });
            _gameplayWindowsSetActions.Add("UI_BeesWaxProduceConstructionMenu", () => {
                _UI_BeesWaxProduceConstructionMenu.SetActive(true);
                _UI_BeesWaxProduceConstructionMenu.GetComponent<UI_BeesWaxProduceConstructionMenu>()._CallMenu(_construction);
            });*/
    }
}
