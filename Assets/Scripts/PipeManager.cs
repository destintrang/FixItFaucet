using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{


    //Record the answer for the current pipe layout
    Vector2Int answer;


    private int startingRow = 6;
    private int endingRow = 0;
    private const int COLNUM = 8;
    private const int ROWNUM = 6;



    //Singleton
    public static PipeManager instance;
    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {

        LoadImages(LoadPipes());
        Timer.instance.StartTimer();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //When the player sends in an answer
    public void SubmitAnswer (Vector2Int a)
    {

        if (a == answer)
        {

            StartCoroutine(OnClear());

        }
        else
        {

            Cursor.instance.Incorrect();
            SFXManager.instance.PlayIncorrect();

        }

    }


    IEnumerator OnClear ()
    {


        //Disable player real quick
        PlayerController.instance.enabled = false;

        Cursor.instance.Connect();
        Timer.instance.StopTimer();


        //Delay for connecting the pipe
        float counter = 0f;
        float connectDelay = 30;
        while (counter < connectDelay)
        {
            counter++;
            yield return null;
        }


        //Play correct SFX
        SFXManager.instance.PlayCorrect();

        //Increase stats
        Score.instance.IncrementCleared();
        Score.instance.IncrementScore((int)Timer.instance.GetTimePercentage());

        //Resets
        Cursor.instance.ResetCursorPipe();
        PlayerController.instance.ResetPlayerController();

        //Set the new starting row
        startingRow = endingRow;

        DestroyOldPipes();

        //Delay between loading new pipes
        counter = 0f;
        float loadDelay = 60;
        while (counter < connectDelay)
        {
            counter++;
            yield return null;
        }

        //Enable player
        PlayerController.instance.enabled = true;

        LoadImages(LoadPipes());
        Timer.instance.StartTimer();

    }


    List<Vector2Int> LoadPipes ()
    {

        List<Vector2Int> pipes = new List<Vector2Int>();

        //Add starting pipe
        Vector2Int currentPipe = new Vector2Int(1, startingRow);
        pipes.Add(currentPipe);
        Vector2Int lastPipeDirection = new Vector2Int(0, 0);

        //Randomly determine the goal pipe
        int randomRow = Random.Range(startingRow - 3, startingRow + 3);
        randomRow = Mathf.Clamp(randomRow, 1, 6);
        Vector2Int goalPipe = new Vector2Int(COLNUM, Random.Range(1, 7));
        endingRow = goalPipe.y;

        while (true)
        {

            //If we're on the last column, just go to the goal
            if (currentPipe.x == COLNUM)
            {

                //Go up
                if (currentPipe.y < goalPipe.y)
                {

                    Vector2Int dir = new Vector2Int(0, 1);

                    //Update the new pipe
                    currentPipe = currentPipe + dir;
                    pipes.Add(currentPipe);
                    lastPipeDirection = dir * -1;

                }
                //Go down
                else if (currentPipe.y > goalPipe.y)
                {

                    Vector2Int dir = new Vector2Int(0, -1);

                    //Update the new pipe
                    currentPipe = currentPipe + dir;
                    pipes.Add(currentPipe);
                    lastPipeDirection = dir * -1;

                }

            }


            //Otherwise move normally
            else  {
                //Pipe can possibly go up, down, left, upleft, downleft
                List<Vector2Int> possibleDirections = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, -1) };
                
                //Get rid of all invalid moves
                //Make sure we never backtrack
                possibleDirections.Remove(lastPipeDirection);
                //Make sure to not go out of bounds
                if (currentPipe.y == 1)
                {
                    possibleDirections.Remove(new Vector2Int(0, -1));
                    possibleDirections.Remove(new Vector2Int(1, -1));
                }
                else if (currentPipe.y == ROWNUM)
                {
                    possibleDirections.Remove(new Vector2Int(0, 1));
                    possibleDirections.Remove(new Vector2Int(1, 1));
                }

                //Choose a random available direction
                Vector2Int randomDirection = possibleDirections[Random.Range(0, possibleDirections.Count)];

                //Update the new pipe
                currentPipe = currentPipe + randomDirection;
                pipes.Add(currentPipe);
                lastPipeDirection = randomDirection * -1;
            }


            if (currentPipe == goalPipe) { break; }

        }

        //foreach(Vector2Int p in pipes) { Debug.Log(p); }
        return pipes;

    }


    void LoadImages (List<Vector2Int> pipes)
    {


        //While we loop through setting up images, we can set the answer for this pipe set
        int missingPipe = Random.Range(pipes.Count * 2 / 3, pipes.Count - 1);


        //Load image for first pipe
        Vector2Int tempPipe = pipes[0] - new Vector2Int(1, 0);
        float tempAngle = Vector2.Angle(pipes[0] - tempPipe, pipes[0] - pipes[1]);
        int tempRotation = (ConvertToDirection(tempPipe, pipes[0], pipes[1]));
        GameObject f = Instantiate(ConvertToPipe(tempAngle, tempRotation), new Vector3(pipes[0].x, pipes[0].y, 1), Quaternion.identity);
        //Play its fade in animation
        f.GetComponent<Animator>().Play("Fade In");

        for (int i = 1; i < pipes.Count - 1; i++)
        {

            Vector2Int nextPipe = pipes[i + 1];

            float pipeAngle = Vector2.Angle(pipes[i] - pipes[i - 1], pipes[i] - pipes[i + 1]);
            int pipeRotation = (ConvertToDirection(pipes[i - 1], pipes[i], pipes[i + 1]));
            //THIS is the pipe that goes missing
            if (i == missingPipe - 1)
            {

                GameObject m = Instantiate(ConvertToPipe(pipeAngle, pipeRotation), new Vector3(pipes[i].x, pipes[i].y, 1), Quaternion.identity);
                //Play its fade in animation
                m.GetComponent<Animator>().Play("Fade In");

                //Save the answer HERE
                answer = new Vector2Int((int)pipeAngle, pipeRotation);
                //Update the cursor position
                Cursor.instance.MoveCursor(pipes[i]);

                foreach(SpriteRenderer s in m.GetComponentsInChildren<SpriteRenderer>())
                {
                    s.enabled = false;
                }

            }
            else
            {
                GameObject  p = Instantiate(ConvertToPipe(pipeAngle, pipeRotation), new Vector3(pipes[i].x, pipes[i].y, 1), Quaternion.identity);
                //Play its fade in animation
                p.GetComponent<Animator>().Play("Fade In");
            }

        }

        //Load image for last pipe
        tempPipe = pipes[pipes.Count - 1] + new Vector2Int(1, 0);
        tempAngle = Vector2.Angle(pipes[pipes.Count - 1] - pipes[pipes.Count - 2], pipes[pipes.Count - 1] - tempPipe);
        tempRotation = (ConvertToDirection(pipes[pipes.Count - 2], pipes[pipes.Count - 1], tempPipe));
        GameObject l = Instantiate(ConvertToPipe(tempAngle, tempRotation), new Vector3(pipes[pipes.Count - 1].x, pipes[pipes.Count - 1].y, 1), Quaternion.identity);
        //Play its fade in animation
        l.GetComponent<Animator>().Play("Fade In");

    }


    int ConvertToDirection (Vector2Int last, Vector2Int current, Vector2Int next)
    {

        List<Vector2Int> directions = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1), };

        int counter = 0;
        int l = 0;
        int n = 0;

        foreach (Vector2Int dir in directions)
        {

            counter++;

            Vector2Int check = dir + current;
            if (check == last)
            {
                l = counter;
            }
            else if (check == next)
            {
                n = counter;
            }


        }

        int difference = Mathf.Abs(l - n);
        if (difference > 4) { return Mathf.Max(l, n); }
        else { return Mathf.Min(l, n); }

    }


    GameObject ConvertToPipe (float angle, int rotation)
    {

        string pipe = angle.ToString() + " " + rotation.ToString();

        return (Resources.Load<GameObject>("Pipes/" + pipe));

    }


    void DestroyOldPipes ()
    {

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            p.GetComponent<Animator>().Play("Fade Out");
        }

    }


}
