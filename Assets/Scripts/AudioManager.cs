using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    [Header("Manager")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioSource normalSound;
    [SerializeField] private AudioSource adjustedSound;


    [Header("Music")]
    public AudioClip background;

    [Header("Player Sound")]
    public AudioClip _playerAttack;
    public AudioClip _playerHit;
    public AudioClip _playerExplode;
    public AudioClip boostEngine;
    [Header("UI Sound")]
    public AudioClip _uiPause;
    public AudioClip _uiUnpause;
    [Header("Object Sound")]
    public AudioClip _explode1;
    public AudioClip _hitBullet;
    public AudioClip _hit3;
    public AudioClip _hit4;

    private void Start()
    {
        music.clip = background;
        music.Play();
    }

    public void PlaySound(AudioClip sfx)
    {
        normalSound.PlayOneShot(sfx);
    }

    public void PlayAdjustedSound(AudioClip sfx)
    {
        adjustedSound.PlayOneShot(sfx);
    }

    public void StopSound()
    {
        sound.Stop();
    }
}
