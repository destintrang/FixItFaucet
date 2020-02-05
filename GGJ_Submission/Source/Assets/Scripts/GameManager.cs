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

        gameOverScreen.SetActive(true);
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


}
