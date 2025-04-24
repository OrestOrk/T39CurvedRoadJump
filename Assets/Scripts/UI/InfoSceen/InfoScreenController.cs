using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScreenController : MonoBehaviour
{
    private InfoScreenView _infoScreenView;
    
    private void Start()
    {
        _infoScreenView = GetComponent<InfoScreenView>();
    }

    public void Open()
    {
        _infoScreenView.ShowScreen();
    }

    public void Close()
    {
        _infoScreenView.HideScreen();
    }
}
