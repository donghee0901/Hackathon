using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainActivity : MonoBehaviour
{
    List<int> inputQueue = new List<int>();
    bool isMoving = false;

    enum INPUTKEY
    {
        UP,     //0
        DOWN,   //1
        LEFT,  //2
        RIGHT    //3
    }
    
    void Start()
    {
        inputQueue.Clear();
        Debug.Log("Main Loaded");
    }
    
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
            if (!isMoving)
            {
                isMoving = true;
                string output = "";

                foreach (int i in inputQueue)
                {
                    output = output + i + " ";
                }

                Debug.Log(output);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if ((inputQueue.Count - 1) >= 0)
            {
                Debug.Log("deleting at " + (inputQueue.Count - 1).ToString());
                inputQueue.RemoveAt(inputQueue.Count - 1);
            }
        }

        //디버깅
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            string debugString = "";

            foreach(int i in inputQueue)
            {
                debugString = debugString + i + " ";
            }

            Debug.Log(debugString);
        }
    }
}