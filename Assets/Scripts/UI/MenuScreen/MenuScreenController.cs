using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenController : MonoBehaviour
{
    private MenuScreenView _menuScreenView;
    private GameController _gameController;
    
    private void Start()
    {
        _menuScreenView = GetComponent<MenuScreenView>();
        
        _gameController = ServiceLocator.GetService<GameController>();

        _gameController.OnStartPlaying += _menuScreenView.HideMenu;
        _gameController.OnResetGame += _menuScreenView.ShowMenu;
    }

    private void OnDestroy()
    {
        _gameController.OnStartPlaying -= _menuScreenView.HideMenu;
        _gameController.OnResetGame -= _menuScreenView.ShowMenu;
    }
}
