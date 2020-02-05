using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{


    [SerializeField] protected float startingTime;
    private float currentMaxTime = 0;
    private float step = 1;
    [SerializeField] protected float stepMultiplier = 1.5f;
    private float counter;



    //Singleton
    public static Timer instance;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public float GetTimePercentage ()
    {
        return (1 - (counter / currentMaxTime)) * 100;
    }


    public void StartTimer ()
    {
        StopAllCoroutines();
        StartCoroutine(StartTimerCoroutine());
        HastenStep();
    }
    public void StopTimer ()
    {
        StopAllCoroutines();
    }


    IEnumerator StartTimerCoroutine ()
    {

        counter = 0f;
        currentMaxTime = startingTime;

        while (counter < currentMaxTime)
        {
            counter += step;
            yield return null;
        }

        GameOver();

    }

    private void GameOver ()
    {

        GameManager.instance.LoadGameOverScreen();
        //Time.timeScale = 0f;
        PlayerController.instance.enabled = false;

    }

    private void HastenStep ()
    {
        step *= stepMultiplier;
    }


}
