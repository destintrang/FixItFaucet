using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{


    /// <summary>
    /// KEEPS TRACK OF THE PLAYER'S PROGRESS OVER ALL RUNS
    /// </summary>


    public int clearedHighscore = 0;
    public int highscore = 0;

    public int GetMaxCleared () { return clearedHighscore; }
    public int GetMaxScore () { return highscore; }



    public static StatsManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ImportPlayerData(SaveManager.LoadData());
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateStats (int pipes, int score)
    {

        clearedHighscore = Mathf.Max(pipes, clearedHighscore);
        highscore = Mathf.Max(score, highscore);

    }

    

    //Load stats saved on the computer
    public void ImportPlayerData(PlayerData data)
    {

        clearedHighscore = data.clearedHighscore;
        highscore = data.highscore;

    }
    //Create and return a PlayerData object initialized with this object
    public PlayerData ExportPlayerData()
    {
        return new PlayerData(this);
    }

}
