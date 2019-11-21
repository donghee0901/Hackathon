using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput : MonoBehaviour
{
    List<int> inputQueue = new List<int>();

    enum INPUTKEY
    {
        UP,     //0
        DOWN,   //1
        LEFT,  //2
        RIGHT    //3
    }

    // Start is called before the first frame update
    void Start()
    {
        inputQueue.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputQueue.Add((int)INPUTKEY.UP);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputQueue.Add((int)INPUTKEY.DOWN);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inputQueue.Add((int)INPUTKEY.LEFT);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputQueue.Add((int)INPUTKEY.RIGHT);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            string output = "";

            foreach (int i in inputQueue)
            {
                output = output + i + " ";
            }

            Debug.Log(output);
        }

        ////Debuging
        //if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    string debug = "";

        //    foreach(int i in inputQueue)
        //    {
        //        debug = debug + i + " ";
        //    }

        //    Debug.Log(debug);
        //}
    }
}
