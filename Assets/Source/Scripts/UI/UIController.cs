using System;
using BugStrategy.Constructions;
using BugStrategy.Missions.MissionEditor.UI;
using BugStrategy.UI.Elements.EntityInfo.ConstructionInfo;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;
using BugStrategy.UI.Screens;
using BugStrategy.Unit;
using BugStrategy.Unit.UnitSelection;
using CycleFramework.Screen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BugStrategy.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIScreenRepository))]
    public class UIController : MonoBehaviour
    {
        private GameObject _uiActiveScreen;
        private GameObject _uiPrevActiveScreen;
        private UIScreenRepository _screenRepository;

        private static UI_ERROR _uiError;
    
        private UnitInfoScreen _unitInfoScreen;
        private ConstructionInfoScreen _constructionInfoScreen;

        private void Awake()
        {
            _screenRepository = GetComponent<UIScreenRepository>();
            _screenRepository.Initialize();
        }

        private void Start()
        {
            var screenTypes = new Type[]
            {
                typeof(UI_Gameplay),
                typeof(UI_GameplayMenu),
                typeof(UI_Settings),
                typeof(UI_GameplayWin),
                typeof(UI_GameplayLose),
                typeof(UI_MainMenu),
                typeof(UI_NewGame),
                typeof(UI_Saves),
                
                typeof(UI_MissionEditor),
                typeof(UI_MissionEditorSaves),
            };

            //определяем, какое окно у нас активно при запуске.
            foreach (var screenType in screenTypes)
                if (IsActive(screenType, out _uiActiveScreen))
                    break;

            if (_uiActiveScreen == null)
                throw new NullReferenceException("Cant find any active screen");

            _uiPrevActiveScreen = _uiActiveScreen;

            _uiError =  _screenRepository.GetScreen<UI_ERROR>();
        
            _unitInfoScreen = _screenRepository.GetScreen<UnitInfoScreen>();
            _constructionInfoScreen = _screenRepository.GetScreen<ConstructionInfoScreen>();

            if(_screenRepository.GetScreen<UI_Gameplay>() != null)
                SetScreen(UIScreenType.Gameplay);
        }
        
        /// <param name="screenObj"> equal to null, if screen un active</param>
        private bool IsActive(Type screenType, out GameObject screenObj)
        {
            screenObj = null;
            var screen = _screenRepository.GetScreen(screenType);

            if (screen != null && screen.isActiveAndEnabled)
            {
                screenObj = screen.gameObject;
                return true;
            }
            
            return false;
        }

        public bool IsConstructionInfoScreenActive ()
        {
            return _constructionInfoScreen.GetActiveSelf();
        }

        public void SetScreen(UnitBase unitBase)
        {
            _constructionInfoScreen.Hide();
            _unitInfoScreen.Show();
            _unitInfoScreen.SetUnit(unitBase);
        }

        public void SetScreen(ConstructionBase construction)
        {
            _unitInfoScreen.Hide();
            _constructionInfoScreen.Show();
            _constructionInfoScreen.SetConstruction(construction);
        }
        
        public void SetScreen(string screenTypeName)
        {
            if (Enum.TryParse(screenTypeName, out UIScreenType screenType)) 
                SetScreen(screenType);
        }

        public void SetScreen(UIScreenType type)
        {
            var screenBuffer = _uiActiveScreen;
            _uiActiveScreen.SetActive(false);

            switch (type)
            {
                case UIScreenType.Gameplay:
                    _unitInfoScreen.Hide();
                    _constructionInfoScreen.Hide();
                    _uiActiveScreen = _screenRepository.GetScreen<UI_Gameplay>().gameObject; 
                    break;
                case UIScreenType.GameplayMenu:
                    _uiActiveScreen = _screenRepository.GetScreen<UI_GameplayMenu>().gameObject; 
                    break;
                case UIScreenType.Settings:
                    _uiActiveScreen = _screenRepository.GetScreen<UI_Settings>().gameObject; 
                    break;
                case UIScreenType.GameplayWin:
                    _uiActiveScreen = _screenRepository.GetScreen<UI_GameplayWin>().gameObject; 
                    break;
                case UIScreenType.GameplayLose:
                    _uiActiveScreen = _screenRepository.GetScreen<UI_GameplayLose>().gameObject; 
                    break;
                case UIScreenType.MainMenu:
                    _uiActiveScreen = _screenRepository.GetScreen<UI_MainMenu>().gameObject;
                    break;
                case UIScreenType.NewGame:
                    _uiActiveScreen = _screenRepository.GetScreen<UI_NewGame>().gameObject;
                    break;
                case UIScreenType.Saves:
                    _uiActiveScreen = _screenRepository.GetScreen<UI_Saves>().gameObject; 
                    break;
                case UIScreenType.Back:
                    _uiActiveScreen = _uiPrevActiveScreen; 
                    break;
                case UIScreenType.MissionEditor:
                    _uiActiveScreen = _screenRepository.GetScreen<UI_MissionEditor>().gameObject; 
                    break;
                case UIScreenType.MissionEditorSaves:
                    _uiActiveScreen = _screenRepository.GetScreen<UI_MissionEditorSaves>().gameObject; 
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            _uiPrevActiveScreen = screenBuffer;
            _uiActiveScreen.SetActive(true);
        }

        public static void ErrorCall(string error) 
            => _uiError.ErrorCall(error);
    }
}
