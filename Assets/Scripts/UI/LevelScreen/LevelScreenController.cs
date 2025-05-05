using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreenController : MonoBehaviour
{
    private LevelScreenView _view;

    private void Start()
    {
        _view = GetComponent<LevelScreenView>();
    }

    public void ShowLevelScreen()
    {
        _view.ShowLevelScreen();
    }

    public void HideLevelScreen()
    {
        _view.HideLevelScreen();
    }
}
