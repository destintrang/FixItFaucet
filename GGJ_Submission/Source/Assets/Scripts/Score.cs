using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{


    private int currentScore = 0;
    private int pipesCleared = 0;


    //Singleton
    public static Score instance;
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


    public int GetCleared() { return pipesCleared; }
    public int GetScore() { return currentScore; }
    public void IncrementScore (int s)
    {
        currentScore += s;
    }
    public void IncrementCleared ()
    {
        pipesCleared++;
    }



    public void ArchiveScore()
    {
        if (StatsManager.instance != null)
        {
            StatsManager.instance.UpdateStats(currentScore, pipesCleared);
        }
    }


}
