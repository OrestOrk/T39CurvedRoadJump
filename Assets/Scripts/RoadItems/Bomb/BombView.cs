using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionVFX;
    [SerializeField] private GameObject _bombModel;
    
    public void ActivationEffect()
    {
        _explosionVFX.Play();
        
        DelayManager.DelayAction(HideModel,0.3f);
    }

    private void HideModel()
    {
        _bombModel.SetActive(false);
    }
}
