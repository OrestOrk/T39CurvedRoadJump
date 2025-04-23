using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _failScreen;

    public void Show()
    {
        _failScreen.SetActive(true);
    }

    public void Hide()
    {
        _failScreen.SetActive(false);
    }
}
