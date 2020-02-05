using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    AudioSource a;

    float originalPitch;

    [SerializeField] protected AudioClip rotation;
    [SerializeField] protected AudioClip toggle;
    [SerializeField] protected AudioClip connect;
    [SerializeField] protected AudioClip correct;
    [SerializeField] protected AudioClip incorrect;


    public static SFXManager instance;
    private void Awake()
    {
        instance = this;

        //Reference calls
        a = GetComponent<AudioSource>();
        originalPitch = a.pitch;
    }

    
    public void Play (AudioClip sfx)
    {
        Debug.Log("???");
        a.PlayOneShot(sfx);
    }

    public void PlayRotation()
    {
        ResetPitch();
        //a.pitch *= Random.Range(0.8f, 1.2f);
        a.PlayOneShot(rotation);
    }
    public void PlayToggle()
    {
        ResetPitch();
        //a.pitch *= Random.Range(0.8f, 1.2f);
        a.PlayOneShot(toggle);
    }
    public void PlayConnect()
    {
        ResetPitch();
        a.PlayOneShot(connect);
    }
    public void PlayCorrect()
    {
        ResetPitch();
        a.PlayOneShot(correct);
    }
    public void PlayIncorrect()
    {
        ResetPitch();
        a.PlayOneShot(incorrect);
    }


    //Reset the pitch to the base pitch before each SFX
    private void ResetPitch ()
    {
        a.pitch = originalPitch;
    }

}
