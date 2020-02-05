using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{


    //Bool telling whether this is active
    private bool active = false;
    public bool startActive;


    [SerializeField] protected string posButton;
    [SerializeField] protected string negButton;


    //Stuff for the selector
    [SerializeField] protected RectTransform selector;
    [SerializeField] protected List<RectTransform> options;
    private int optionIndex = 0;

    //To handle fast inputs
    float inputCooldown = 14;
    float cooldownCounter = 0;


    [SerializeField] protected GameObject pauseScreen;




    // Start is called before the first frame update
    void Start()
    {
        //if (startActive) { Activate(); }
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckForPause();
        CheckForJoystickMovement();
        CheckForSubmitButton();
    }


    //private void CheckForPause()
    //{
    //    if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Pause"))
    //    {
    //        TogglePause();
    //    }
    //}

    private void CheckForJoystickMovement()
    {

        //If there's only 1 option, theres no need for button navi
        if (options.Count < 2) { return; }

        //If button navi is on cooldown, return
        if (!active) { return; }
        if (cooldownCounter < inputCooldown)
        {
            cooldownCounter += 1;
            return;
        }


        //Check for joystick input
        int input = GetInput();
        if (input < 0)
        {
            MoveSelector(1);
            MenuSFXManager.instance.PlayButtonNavi();
            cooldownCounter = 0;
        }
        else if (input > 0)
        {
            MoveSelector(-1);
            MenuSFXManager.instance.PlayButtonNavi();
            cooldownCounter = 0;
        }


    }
    private void CheckForSubmitButton()
    {

        if (!active) { return; }

        //Check for submit input
        if (GetSubmitButton())
        {
            OnSubmitButton();
            MenuSFXManager.instance.PlayButtonSubmit();
            cooldownCounter = 0;
        }

    }


    public void Activate()
    {

        if (!active)
        {
            //Toggle pause on
            active = true;
            //Set up the UI
            ResetSelector();
        }
        else if (active)
        {
            //Toggle pause off
            active = false;
        }

    }


    public void QuitGame()
    {
        //LevelManager.instance.EndGame();
    }

    public void RestartGame()
    {
        //LevelManager.instance.RestartLevel();
    }


    private void OnSubmitButton()
    {

        options[optionIndex].GetComponent<Button>().onClick.Invoke();

    }


    private void ResetSelector()
    {

        optionIndex = 0;
        selector.localPosition = new Vector3(options[optionIndex].localPosition.x, options[optionIndex].localPosition.y - 30, options[optionIndex].localPosition.z);

    }
    private void MoveSelector(int direction)
    {

        optionIndex += direction;
        if (optionIndex < 0) { optionIndex = options.Count - 1; }
        else if (optionIndex >= options.Count) { optionIndex = 0; }

        selector.localPosition = new Vector3(options[optionIndex].localPosition.x, options[optionIndex].localPosition.y - 30, options[optionIndex].localPosition.z);

    }


    public void Recalibrate ()
    {

        selector.localPosition = new Vector3(options[optionIndex].localPosition.x, options[optionIndex].localPosition.y - 30, options[optionIndex].localPosition.z);

    }



    public int GetInput()
    {


        if (Input.GetButton(posButton)) { return 1; }
        else if (Input.GetButton(negButton)) { return -1; }
        else { return 0; }


    }
    public bool GetSubmitButton()
    {
        return Input.GetButtonDown("Submit");
    }

}
