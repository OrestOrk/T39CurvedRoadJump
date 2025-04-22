using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _shopCamera; 
    [SerializeField] private GameObject _shopCanvas;
    [SerializeField] private GameObject _overlayCanvas;

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
}
