using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;
    private float audioTime;
    private AudioClip currentStatePlayer, currentStateDog, currentStateDoor;
    [Header("Audio Source")]
    [SerializeField] private AudioSource SFXEffectSource;
    [SerializeField] private AudioSource MusicSource;
    [Header("Audio Clip")]
    [SerializeField] private AudioClip backgroundMusic;
    public AudioClip deadth;
    public AudioClip jump;
    public AudioClip climb;
    public AudioClip run;
    public AudioClip rock;
    public AudioClip DoorOpen;
    [Header("Dog Clip")]
    public AudioClip dogWol;
    public AudioClip dogDead;
    [Header("Bow Clip")]
    public AudioClip BowShot;
    [Header("Grenade Clip")]
    public AudioClip Grenade;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        MusicSource.clip = backgroundMusic;
        MusicSource.Play();
        MusicSource.loop = true;
    }
    public void SoundEffect(AudioClip clip)
    {
        SFXEffectSource.PlayOneShot(clip);
    }



}

