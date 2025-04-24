using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsController : MonoBehaviour
{
    public event Action<bool> OnMusicToggled;
    public event Action<bool> OnSoundToggled;
    
    public event Action<float> OnMusicVolumeChanged;
    public event Action<float> OnSoundVolumeChanged;

    private bool _isMusicOn = true;
    private bool _isSoundOn = true;

    private SettingsView _settingsView;
    private void Start()
    {
        _settingsView = GetComponent<SettingsView>();

        _settingsView.musicVolumeSlider.onValueChanged.AddListener(HandleMusicSliderChanged);
        _settingsView.soundVolumeSlider.onValueChanged.AddListener(HandleSoundSliderChanged);
    }
    
    private void HandleMusicSliderChanged(float value)
    {
        Debug.Log(value);
        
        bool isOn = value > 0.001f;
        if (_isMusicOn != isOn)
        {
            _isMusicOn = isOn;
            OnMusicToggled?.Invoke(_isMusicOn);
            
            _settingsView.ControllMusicIcon(_isMusicOn);
        }

        OnMusicVolumeChanged?.Invoke(value);
    }

    private void HandleSoundSliderChanged(float value)
    {
        bool isOn = value > 0.001f;
        if (_isSoundOn != isOn)
        {
            _isSoundOn = isOn;
            OnSoundToggled?.Invoke(_isSoundOn);
            
            _settingsView.ControllSoundIcon(_isSoundOn);
        }
        

        OnSoundVolumeChanged?.Invoke(value);
    }

    public void OpenPanel()
    {
        _settingsView.ShowScreen();
    }

    public void ClosePanel()
    {
        _settingsView.HideScreen();
    }
}