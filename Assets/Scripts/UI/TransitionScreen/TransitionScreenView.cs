using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _transitionScreen;

    public void ShowTransitionScreen()
    {
        _transitionScreen.SetActive(true);
        
        DelayManager.DelayAction(HideTransitionScreen,2.2f);
    }

    private void HideTransitionScreen()
    {
        _transitionScreen.SetActive(false);
    }
}
