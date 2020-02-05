using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{


    public int clearedHighscore;
    public int highscore;


    public PlayerData()
    {
        clearedHighscore = 0;
        highscore = 0;
    }
    public PlayerData (StatsManager s)
    {
        clearedHighscore = s.GetMaxCleared();
        highscore = s.GetMaxScore();
    }


    //Singleton
    public static PlayerData instance;
    private void Awake()
    {
        if (instance == null)
        {
            //CopyData(SaveManager.LoadData());
            instance = this;
        }
    }


    //public void InputResult (ScoreManager s)
    //{

    //    highScore = Mathf.Max(highScore, s.GetScore());
    //    highestWavesCleared = s.GetClearedWaves();
    //    runs++;

    //}


    //private void CopyData (PlayerData p)
    //{
    //    highScore = p.highScore;
    //    runs = p.runs;
    //}

}
