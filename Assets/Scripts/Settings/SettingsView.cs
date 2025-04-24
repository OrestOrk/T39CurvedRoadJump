using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsView : BaseMiniScreens
{
    public Slider soundVolumeSlider => _soundVolumeSlider;
    public Slider musicVolumeSlider => _musicVolumeSlider;
    
    [Header("GameObjects")]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _settingsWindow;
    
    [Header("SlidersUI")]
    [SerializeField] private Slider _soundVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    [Header("IconsUI")]
    [SerializeField] private Image _soundIcon;
    [SerializeField] private Image _musicIcon;

    [Header("MusicSpites")]
    [SerializeField] private Sprite[] _musicSprites;
    
    [Header("SoundSpites")]
    [SerializeField] private Sprite[] _soundSprites;
    
    
    private float _animationDuration = 0.5f;
    

    public void ControllMusicIcon(bool state)
    {
        _musicIcon.sprite = state ? _musicSprites[0] : _musicSprites[1];
    }

    public void ControllSoundIcon(bool state)
    {
        _soundIcon.sprite = state ? _soundSprites[0] : _soundSprites[1];
    }
}
