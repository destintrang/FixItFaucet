using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{


    /// <summary>
    /// Customize this to hold whatever info, based on the game
    /// </summary>


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
