using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSFXManager : MonoBehaviour
{
    
    AudioSource a;

    [SerializeField] protected AudioClip buttonNavi;
    [SerializeField] protected AudioClip buttonSubmit;




    public static MenuSFXManager instance;
    private void Awake()
    {
        instance = this;
        a = GetComponent<AudioSource>();
    }


    public void PlayButtonNavi()
    {
        a.PlayOneShot(buttonNavi);
    }
    public void PlayButtonSubmit()
    {
        a.PlayOneShot(buttonSubmit);
    }

}
