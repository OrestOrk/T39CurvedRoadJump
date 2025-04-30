using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WheelSectorController : MonoBehaviour
{
    [SerializeField] private int _sectorValue;
    
    public int sectorValue => _sectorValue;

    public bool isGameOver => _isGameOver;
    [SerializeField] private bool _isGameOver;
    
    private WheelSectorView _wheelSectorView;

    private void Start()
    {
        _wheelSectorView = GetComponent<WheelSectorView>();
    }
    public void WinSector()
    {
        _wheelSectorView.ShowWinAnimation();
        
        DelayManager.DelayAction(_wheelSectorView.StopWinAnimation,2f);
    }
}