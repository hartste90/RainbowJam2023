using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip hitClip;
    public AudioClip gainedFollowerClip;
    public AudioClip uiSoundClip;
    public AudioClip deathClip;
    public AudioClip newLifeClip;
    
    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayHitClip()
    {
        sfxSource.PlayOneShot(hitClip);    
    }
    
    public void PlayGainedFollowerClip()
    {
        sfxSource.PlayOneShot(gainedFollowerClip);    
    }
    
    public void PlayUISoundClip()
    {
        sfxSource.PlayOneShot(uiSoundClip);    
    }
    
    public void PlayDeathClip()
    {
        sfxSource.PlayOneShot(deathClip);    
    }
    
    public void PlayNewLifeClip()
    {
        sfxSource.PlayOneShot(newLifeClip);    
    }
}
