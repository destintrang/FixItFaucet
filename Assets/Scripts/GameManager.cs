using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    [SerializeField] protected GameObject gameOverScreen;
    [SerializeField] protected Text endCleared;
    [SerializeField] protected Text endScore;

    [SerializeField] protected ButtonManager endButtons;


    //Singleton
    public static GameManager instance;
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


    public void LoadGameOverScreen ()
    {

        //Update and save player stats here
        Score.instance.ArchiveScore();
        SaveManager.SaveData();

        SFXManager.instance.PlayDeath();
        gameOverScreen.SetActive(true);
        endButtons.Recalibrate();
        StartCoroutine(PausePlayer());


        endCleared.text = Score.instance.GetCleared().ToString();
        endScore.text = Score.instance.GetScore().ToString();

    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Main Game");
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }


    IEnumerator PausePlayer ()
    {


        float counter = 0f;
        float delay = 90f;

        endButtons.enabled = false;

        while (counter < delay)
        {
            counter++;
            yield return null;
        }

        endButtons.enabled = true;


    }


}
