using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{


    private int currentScore = 0;
    private int pipesCleared = 0;

    private int highscore = 0;
    private int highPipesCleared = 0;


    //Singleton
    public static Score instance;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

        LoadHighscores();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int GetCleared() { return pipesCleared; }
    public int GetScore() { return currentScore; }
    public int GetHighCleared() { return highPipesCleared; }
    public int GetHighScore() { return highscore; }
    public void IncrementScore (int s)
    {
        currentScore += s;
        highscore = Mathf.Max(currentScore, highscore);
    }
    public void IncrementCleared ()
    {
        pipesCleared++;
        highPipesCleared = Mathf.Max(pipesCleared, highPipesCleared);
    }


    private void LoadHighscores ()
    {

        PlayerData d = StatsManager.instance.ExportPlayerData();
        highscore = d.highscore;
        highPipesCleared = d.clearedHighscore;

    }


    public void ArchiveScore()
    {
        if (StatsManager.instance != null)
        {
            StatsManager.instance.UpdateStats(pipesCleared, currentScore);
        }
    }


}
