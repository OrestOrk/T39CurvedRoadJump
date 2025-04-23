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
    
    private void Start()
    {
        _transitionScreenController = ServiceLocator.GetService<TransitionScreenController>();
        _gameController = ServiceLocator.GetService<GameController>();
        _failScreenController = ServiceLocator.GetService<FailScreenController>();
        
        _screenView = GetComponent<ScreenView>();

        _gameController.OnGameOver += ShowFailScreen;
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

    private void ShowFailScreen()
    {
        _failScreenController.ShowFailsScreen();
        DelayManager.DelayAction(_transitionScreenController.ShowTransitionScreen,2f);
    }

    private void OnDestroy()
    {
        _gameController.OnGameOver -= ShowFailScreen;
    }
}
