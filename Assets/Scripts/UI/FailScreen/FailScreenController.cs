using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailScreenController : MonoBehaviour
{
    private FailScreenView _view;
    
    //
    private void Start()
    {
        _view = GetComponent<FailScreenView>();
        
       // _gameController = ServiceLocator.GetService<GameController>();

       // _gameController.OnGameOver += ShowFailsScreen;
    }

    public void ShowFailsScreen()
    {
        _view.Show();
        
        DelayManager.DelayAction(_view.Hide,3f);
    }

    private void OnDestroy()
    {
        //_gameController.OnGameOver -= ShowFailsScreen;
    }
}
