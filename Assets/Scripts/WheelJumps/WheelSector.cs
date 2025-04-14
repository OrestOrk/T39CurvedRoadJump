using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WheelSector : MonoBehaviour
{
    [SerializeField] private int _sectorValue;
    
    public int sectorValue => _sectorValue;

    [SerializeField] private Text _text;

    private void Start()
    {
        DisplayText();
    }

    private void DisplayText()
    {
        _text.text = _sectorValue.ToString();
    }
}
