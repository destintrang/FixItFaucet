using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{


    [SerializeField] protected Text timer;

    [SerializeField] protected Text cleared;
    [SerializeField] protected Text score;

    [SerializeField] protected Text clearedHigh;
    [SerializeField] protected Text scoreHigh;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        UpdateTimer();
        UpdateScores();

    }

    void UpdateTimer ()
    {
        timer.text = ((int)Timer.instance.GetTimePercentage()).ToString();
    }
    void UpdateScores ()
    {

        cleared.text = Score.instance.GetCleared().ToString();
        score.text = Score.instance.GetScore().ToString();

        clearedHigh.text = Score.instance.GetHighCleared().ToString();
        scoreHigh.text = Score.instance.GetHighScore().ToString();

    }


}
