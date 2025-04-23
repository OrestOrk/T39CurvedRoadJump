using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreenController : MonoBehaviour
{
    private TransitionScreenView _transitionScreenView;
    private void Start()
    {
        _transitionScreenView = GetComponent<TransitionScreenView>();
    }

    public void ShowTransitionScreen()
    {
        _transitionScreenView.ShowTransitionScreen();
    }
}
