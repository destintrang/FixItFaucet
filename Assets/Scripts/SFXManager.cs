using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    AudioSource a;

    float originalPitch;
    float originalVolume;

    [SerializeField] protected AudioClip rotation;
    [SerializeField] protected AudioClip toggle;
    [SerializeField] protected AudioClip connect;
    [SerializeField] protected AudioClip correct;
    [SerializeField] protected AudioClip incorrect;
    [SerializeField] protected AudioClip death;


    public static SFXManager instance;
    private void Awake()
    {
        instance = this;

        //Reference calls
        a = GetComponent<AudioSource>();
        originalPitch = a.pitch;
        originalVolume = a.volume;
    }

    
    public void Play (AudioClip sfx)
    {
        Debug.Log("???");
        a.PlayOneShot(sfx);
    }

    public void PlayRotation()
    {
        ResetAudioSettings();
        //a.pitch *= Random.Range(0.8f, 1.2f);
        a.PlayOneShot(rotation);
    }
    public void PlayToggle()
    {
        ResetAudioSettings();
        //a.pitch *= Random.Range(0.8f, 1.2f);
        a.PlayOneShot(toggle);
    }
    public void PlayConnect()
    {
        ResetAudioSettings();
        a.PlayOneShot(connect);
    }
    public void PlayCorrect()
    {
        ResetAudioSettings();
        a.PlayOneShot(correct);
    }
    public void PlayIncorrect()
    {
        ResetAudioSettings();
        a.volume *= 2.2f;
        a.PlayOneShot(incorrect);
    }
    public void PlayDeath()
    {
        ResetAudioSettings();
        a.volume *= 2.2f;
        a.PlayOneShot(death);
    }



    private void ResetAudioSettings ()
    {
        ResetPitch();
        ResetVolume();
    }
    //Reset the pitch to the base pitch before each SFX
    private void ResetPitch()
    {
        a.pitch = originalPitch;
    }//Reset the volume
    private void ResetVolume()
    {
        a.volume = originalVolume;
    }

}
