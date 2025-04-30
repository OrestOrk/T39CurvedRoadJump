using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSectorView : MonoBehaviour
{
    [SerializeField] private GameObject _winSector;
    
    public void ShowWinAnimation()
    {
        _winSector.SetActive(true);
    }

    public void StopWinAnimation()
    {
        _winSector.SetActive(false);
    }
}
