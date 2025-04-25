using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _shopCamera;
    [SerializeField] private GameObject _dailyBonusCamera;
    [SerializeField] private GameObject _shopCanvas;
    [SerializeField] private GameObject _overlayCanvas;
    [SerializeField] private GameObject _dailyBonusCanvas;
    
    public void OpenShopScreen()
    {
        _shopCanvas.SetActive(true);
        _shopCamera.SetActive(true);
        _overlayCanvas.SetActive(false);
        _mainCamera.SetActive(false);
    }

    public void CloseShopScreen()
    {
        _shopCanvas.SetActive(false);
        _shopCamera.SetActive(false);
        _overlayCanvas.SetActive(true);
        _mainCamera.SetActive(true);
        
        Debug.Log("Shop screen closed");
    }

    public void OpenDailyBonusScreen()
    {
        _dailyBonusCanvas.SetActive(true);
        _dailyBonusCamera.SetActive(true);
        
        _overlayCanvas.SetActive(false);
        _mainCamera.SetActive(false);
    }

    public void CloseDailyBonusScreen()
    {
        _dailyBonusCanvas.SetActive(false);
        _dailyBonusCamera.SetActive(false);
        
        _overlayCanvas.SetActive(true);
        _mainCamera.SetActive(true);
    }
}
