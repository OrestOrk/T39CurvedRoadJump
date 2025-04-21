using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using CartoonFX;

public class ChestView : MonoBehaviour
{
     [SerializeField] private CFXR_ParticleText _particleText;
     [SerializeField] private ParticleSystem _particleTextVFX;
     
     [SerializeField] private ParticleSystem _openChestVFX;
     [SerializeField] private ParticleSystem _coinsVFX;
     private Animator _chestAnimator;

     private float _animationHeight = 3f;
     private float _animationDuration = 1f;
     
     private void Start()
     {
          _chestAnimator = GetComponent<Animator>();
     }

     public void OpenChest()
     {
          _chestAnimator.SetTrigger("Open");
          transform.DOMoveY(_animationHeight, _animationDuration);
          
          _openChestVFX.Play();
          PlayCoinsVFX();
     }

     public void DisplayCoins(int coins)
     {
         _particleText.UpdateText(coins.ToString());
          _particleTextVFX.Play();
     }

     private void PlayCoinsVFX()
     {
          _coinsVFX.Play();
     }
}
