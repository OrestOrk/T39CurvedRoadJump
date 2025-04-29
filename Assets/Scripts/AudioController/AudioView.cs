using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioView : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _soundAudioSource;

    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _lose;
    [SerializeField] private AudioClip _chestOpen;
    [SerializeField] private AudioClip _jumpStart;
    [SerializeField] private AudioClip _bonusJump;
    [SerializeField] private AudioClip _explosionBomb;
    [SerializeField] private AudioClip _trap;
    [SerializeField] private AudioClip _purchaseClip;
    [SerializeField] private AudioClip _selectClip;
    [SerializeField] private AudioClip _jumpEndClip;

    // Публічні властивості для доступу до приватних полів
    public AudioSource MusicAudioSource => _musicAudioSource;
    public AudioSource SoundAudioSource => _soundAudioSource;

    public AudioClip ButtonClick => _buttonClick;
    public AudioClip Lose => _lose;
    public AudioClip ChestOpen => _chestOpen;
    public AudioClip JumpStart => _jumpStart;
    public AudioClip BonusJump => _bonusJump;
    public AudioClip ExplosionBomb => _explosionBomb;
    public AudioClip Trap => _trap;
    public AudioClip PurchaseClip => _purchaseClip;
    public AudioClip SelectClip => _selectClip;
    public AudioClip JumpEndClip => _jumpEndClip;
}