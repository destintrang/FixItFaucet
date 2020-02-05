using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    //The current answer the player has stored
    Vector2Int playerAnswer = new Vector2Int(0, 1);



    //Singleton
    public static PlayerController instance;
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
        CheckForInputs();
    }


    void CheckForInputs ()
    {

        if (Input.GetButtonDown("Submit"))
        {
            OnSubmit();
        }

        if (Input.GetButtonDown("W"))
        {
            OnW();
        }
        if (Input.GetButtonDown("A"))
        {
            OnA();
        }
        if (Input.GetButtonDown("S"))
        {
            OnS();
        }
        if (Input.GetButtonDown("D"))
        {
            OnD();
        }
        if (Input.GetButtonDown("Q"))
        {
            OnQ();
        }
        if (Input.GetButtonDown("E"))
        {
            OnE();
        }

        Cursor.instance.UpdateCurrentPipe(ReturnConvertedAnswer());

    }

    //To account for straight pipes
    Vector2Int ReturnConvertedAnswer ()
    {

        if (playerAnswer.x == 180 && playerAnswer.y > 4)
        {
            return new Vector2Int(playerAnswer.x, playerAnswer.y - 4);
        }
        else { return playerAnswer; }

    }


    void OnSubmit ()
    {

        //If the player hasn't initialized a pipe type then ignore
        if (playerAnswer.x == 0) { return; }

        PipeManager.instance.SubmitAnswer(ReturnConvertedAnswer());

    }

    void OnW()
    {

        //If the player already has this pipe, ignore
        if (playerAnswer.x == 180) { return; }

        //Play toggle sfx
        SFXManager.instance.PlayToggle();

        //The player has chosen the straight pipe
        playerAnswer.x = 180;

    }
    void OnA()
    {

        //If the player already has this pipe, ignore
        if (playerAnswer.x == 135) { return; }

        //Play toggle sfx
        SFXManager.instance.PlayToggle();

        //The player has chosen the obtuse pipe
        playerAnswer.x = 135;

    }
    void OnS()
    {

        //If the player already has this pipe, ignore
        if (playerAnswer.x == 90) { return; }

        //Play toggle sfx
        SFXManager.instance.PlayToggle();

        //The player has chosen the right angle pipe
        playerAnswer.x = 90;

    }
    void OnD()
    {

        //If the player already has this pipe, ignore
        if (playerAnswer.x == 45) { return; }

        //Play toggle sfx
        SFXManager.instance.PlayToggle();

        //The player has chosen the acute pipe
        playerAnswer.x = 45;

    }
    void OnQ()
    {

        //If the player hasn't initialized a pipe type then ignore
        if (playerAnswer.x == 0) { return; }

        //Play rotation sfx
        SFXManager.instance.PlayRotation();

        playerAnswer.y--;
        if (playerAnswer.y == 0) { playerAnswer.y = 8; }

    }
    void OnE()
    {

        //If the player hasn't initialized a pipe type then ignore
        if (playerAnswer.x == 0) { return; }

        //Play rotation sfx
        SFXManager.instance.PlayRotation();

        playerAnswer.y++;
        if (playerAnswer.y == 9) { playerAnswer.y = 1; }

    }


    public void ResetPlayerController ()
    {

        playerAnswer.x = 0;
        playerAnswer.y = 1;

    }


}
