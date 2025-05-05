using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _levelScreen;
    
    public void ShowLevelScreen()
    {
        _levelScreen.SetActive(true);
    }

    public void HideLevelScreen()
    {
        _levelScreen.SetActive(false);
    }
}
