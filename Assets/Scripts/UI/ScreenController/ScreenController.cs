using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    private TransitionScreenController _transitionScreenController;
    
    private ScreenView _screenView;
    private FailScreenController _failScreenController;
    private GameController _gameController;
    private LevelScreenController _levelScreenController;
    private MenuScreenController _menuScreenController;
    
    private void Start()
    {
        _transitionScreenController = ServiceLocator.GetService<TransitionScreenController>();
        _gameController = ServiceLocator.GetService<GameController>();
        _failScreenController = ServiceLocator.GetService<FailScreenController>();
        _levelScreenController = ServiceLocator.GetService<LevelScreenController>();
        _menuScreenController = ServiceLocator.GetService<MenuScreenController>();
        
        _screenView = GetComponent<ScreenView>();

        _gameController.OnGameOver += ShowFailScreen;
        _gameController.OnExitToMenu += ExitToMenuFromPlaying;
    }

    public void OpenShopScreen()
    {
        _transitionScreenController.ShowTransitionScreen();
        
        DelayManager.DelayAction(_screenView.OpenShopScreen,1f);
    }

    public void CloseShopScreen()
    {
        _transitionScreenController.ShowTransitionScreen();
        
       DelayManager.DelayAction(_screenView.CloseShopScreen,1f);
    }

    public void OpenDailyBonusScreen()
    {
        _transitionScreenController.ShowTransitionScreen();
        
        DelayManager.DelayAction(_screenView.OpenDailyBonusScreen,1f);
    }

    public void CloseDailyBonusScreen()
    {
        _transitionScreenController.ShowTransitionScreen();
        
        DelayManager.DelayAction(_screenView.CloseDailyBonusScreen,1f);
    }
    
    //--level screen
    public void ShowLevelScreen()
    {
        _transitionScreenController.ShowTransitionScreen();
        
        DelayManager.DelayAction(_levelScreenController.ShowLevelScreen,1f);
    }

    public void HideLevelScreen()
    {
        _transitionScreenController.ShowTransitionScreen();
        
        DelayManager.DelayAction(_levelScreenController.HideLevelScreen,1f);
    }

    public void ExitToMenuFromPlaying()//exit to menu from play scene
    {
        _transitionScreenController.ShowTransitionScreen();
    }
    private void ShowFailScreen()
    {
        _failScreenController.ShowFailsScreen();
        DelayManager.DelayAction(_transitionScreenController.ShowTransitionScreen,2f);
    }
   
    private void OnDestroy()
    {
        _gameController.OnGameOver -= ShowFailScreen;
        _gameController.OnExitToMenu -= ExitToMenuFromPlaying;
    }
}
