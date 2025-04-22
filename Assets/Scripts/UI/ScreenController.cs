using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    private TransitionScreenController _transitionScreenController;
   // private ShopScreenController _shopScreenController;
    private ScreenView _screenView;
    
    private void Start()
    {
        _transitionScreenController = ServiceLocator.GetService<TransitionScreenController>();
        //_shopScreenController = ServiceLocator.GetService<ShopScreenController>();
        _screenView = GetComponent<ScreenView>();
    }

    public void OpenShopScreen()
    {
        _transitionScreenController.ShowTransitionScreen();
        
        //DelayManager.DelayAction(_shopScreenController.OpenShopScreen,1f);
        DelayManager.DelayAction(_screenView.OpenShopScreen,1f);
    }

    public void CloseShopScreen()
    {
        _transitionScreenController.ShowTransitionScreen();
        
       // DelayManager.DelayAction(_shopScreenController.CloseShopScreen,1f);
       DelayManager.DelayAction(_screenView.CloseShopScreen,1f);
    }
}
