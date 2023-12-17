using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    //Audio
    public AudioSource exploSource;
    public AudioSource finishSound;
    public AudioSource pressSound;
    public AudioSource rollSound;
    public AudioSource gunSound;
    public AudioSource takedameSound;
    public AudioSource healSound;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void PlayExploSound()
    {
        exploSource.Play();
    }

    public void PlayFinishSound()
    {
        finishSound.Play();
    }

    public void PlayPressSound()
    {
        pressSound.Play();
    }

    public void PlayRollSound()
    {
        rollSound.Play();
    }

    public void PlayGunSound()
    {
        gunSound.Play();
    }

    public void PlayTakeDameSound()
    {
        takedameSound.Play();
    }

    public void PlayHealSound()
    {
        healSound.Play();
    }
}
