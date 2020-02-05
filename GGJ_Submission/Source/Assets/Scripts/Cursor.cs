using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{


    [SerializeField] protected Transform cursor;
    [SerializeField] protected Transform pipeObjects;
    [SerializeField] protected Transform pipeObjectsPos;

    [SerializeField] protected List<GameObject> straight;
    [SerializeField] protected List<GameObject> obtuse;
    [SerializeField] protected List<GameObject> right;
    [SerializeField] protected List<GameObject> acute;
    private List<List<GameObject>> allPipes;

    private GameObject currentPipe = null;



    //Singleton
    public static Cursor instance;
    private void Awake()
    {
        instance = this;
        allPipes = new List<List<GameObject>> { straight, obtuse, right, acute };
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetCursorPipe ()
    {

        pipeObjects.position = pipeObjectsPos.position;
        currentPipe.SetActive(false);
        currentPipe = null;

    }


    //Called when we need to animate the pipe connecting
    public void Connect ()
    {

        //Play connect SFX
        SFXManager.instance.PlayConnect();

        pipeObjects.position = cursor.position;
        //Debug.Log(currentPipe.name);
        currentPipe.GetComponent<Animator>().Play("Connect");

    }


    public void UpdateCurrentPipe (Vector2Int p)
    {

        int typeIndex = -1;
        if (p.x == 180) { typeIndex = 0; }
        else if (p.x == 135) { typeIndex = 1; }
        else if (p.x == 90) { typeIndex = 2; }
        else if (p.x == 45) { typeIndex = 3; }
        if (typeIndex < 0) { return; }

        if (currentPipe) currentPipe.SetActive(false);
        currentPipe = allPipes[typeIndex][p.y - 1];
        currentPipe.SetActive(true);

    }

    public void MoveCursor (Vector2Int pos)
    {

        cursor.position = new Vector3(pos.x, pos.y, 1);

    }


}
