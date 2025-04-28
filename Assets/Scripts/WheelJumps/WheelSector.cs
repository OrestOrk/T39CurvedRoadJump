using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WheelSector : MonoBehaviour
{
    [SerializeField] private int _sectorValue;
    
    public int sectorValue => _sectorValue;

    public bool isGameOver => _isGameOver;
    [SerializeField] private bool _isGameOver;
}
