using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    private AudioView _audioView;
    private SettingsController _settingsController;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }    
    private void Start()
    {
        _audioView = GetComponent<AudioView>();
        _settingsController = ServiceLocator.GetService<SettingsController>();

        _settingsController.OnMusicVolumeChanged += HandleMusicVolumeChange;
        _settingsController.OnSoundVolumeChanged += HandleSoundVolumeChange;
    }

    private void HandleMusicVolumeChange(float volume)
    {
        if (_audioView != null && _audioView.MusicAudioSource != null)
        {
            _audioView.MusicAudioSource.volume = Mathf.Clamp01(volume); // Обмеження в межах [0, 1]
        }
    }

    private void HandleSoundVolumeChange(float volume)
    {
        if (_audioView != null && _audioView.SoundAudioSource != null)
        {
            _audioView.SoundAudioSource.volume = Mathf.Clamp01(volume); // Обмеження в межах [0, 1]
        }
    }
    
    // Публічні методи для відтворення звуків

    public void PlayButtonClick()
    {
        PlaySound(_audioView.ButtonClick);
    }

    public void PlayGameOver()
    {
        PlaySound(_audioView.Lose);
    }

    public void PlayChestOpen()
    {
        PlaySound(_audioView.ChestOpen);
    }

    public void PlayJumpStart()
    {
        PlaySound(_audioView.JumpStart);
    }

    public void PlayBonusJump()
    {
        PlaySound(_audioView.BonusJump);
    }

    public void PlayExplosionBomb()
    {
        PlaySound(_audioView.ExplosionBomb);
    }

    public void PlayTrap()
    {
        PlaySound(_audioView.Trap);
    }

    public void PlayPurchaseClip()
    {
        PlaySound(_audioView.PurchaseClip);
    }

    public void PlaySelectClip()
    {
        PlaySound(_audioView.SelectClip);
    }

    public void PlayJumpEndClip()
    {
        PlaySound(_audioView.JumpEndClip);
    }

    public void PlayBonusClip()
    {
        PlaySound(_audioView.BonusClip);
    }

    public void PlaySpinWheelClip()
    {
        PlaySound(_audioView.SpinWheelClip);
    }

    public void PlaySpinEndCLip()
    {
        PlaySound(_audioView.SpinWheelEndClip);
    }

    public void PlayShowScrollClip()
    {
        PlaySound(_audioView.ShopScrollClip);
    }
    
    // Приватний допоміжний метод для відтворення звуків
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && _audioView.SoundAudioSource != null)
        {
            _audioView.SoundAudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null or SoundAudioSource is not assigned.");
        }
    }

    private void OnDestroy()
    {
        
        _settingsController.OnMusicVolumeChanged -= HandleMusicVolumeChange;
        _settingsController.OnSoundVolumeChanged -= HandleSoundVolumeChange;
    }
}