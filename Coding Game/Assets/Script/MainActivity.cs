using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainActivity : MonoBehaviour
{
    private List<INPUTKEY> inputQueue = new List<INPUTKEY>();
    private List<INPUTKEY> UsingQueue;
    enum INPUTKEY
    {
        UP,     //0
        DOWN,   //1
        LEFT,  //2
        RIGHT    //3
    }

    private bool isMoving;
    private Vector3 currentTargetPosition;

    // int[,] map = new int[5,4];

    // private Vector2 current;

    void Start()
    {
        // current = new Vector2();
        inputQueue.Clear();
        Debug.Log("Main Loaded");
    }

    void Update()
    {
        if (isMoving)
        {
            if (transform.position == currentTargetPosition)
            {
                INPUTKEY direction;
                try
                {
                    direction = UsingQueue[0];
                    UsingQueue.RemoveAt(0);
                }
                catch (Exception ignored)
                {
                    isMoving = false;
                    return;
                }
                // int[] moveAmount = MoveCalc(direction);
                Debug.Log(direction);
                /*foreach (var item in moveAmount)
                {
                    Debug.Log(item);
                }
                currentTargetPosition = transform.position + new Vector3(moveAmount[0], moveAmount[1], 0);*/
            }

            transform.position = Vector3.Lerp(transform.position, currentTargetPosition, 10.0f * Time.deltaTime);
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputQueue.Add(INPUTKEY.UP);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputQueue.Add(INPUTKEY.DOWN);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inputQueue.Add(INPUTKEY.LEFT);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputQueue.Add(INPUTKEY.RIGHT);
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

    /*int[] MoveCalc(INPUTKEY direction)
    {
        int xDirection = 0;
        int yDirection = 0;
        int xMove = 0, yMove = 0;
        switch (direction)
        {
            case INPUTKEY.UP:
                xDirection = -1;
                break;
            case INPUTKEY.DOWN:
                xDirection = 1;
                break;
            case INPUTKEY.LEFT:
                yDirection = -1;
                break;
            case INPUTKEY.RIGHT:
                yDirection = 1;
                break;
        }

        while (true)
        {
            current.x += xDirection; // VARIABLE NOT MODIFIABLE
            current.y += yDirection; // VARIABLE NOT MODIFIABLE
            if (current.x < 0)
            {
                current.x = 0;
                break;
            }
            if (current.y < 0)
            {
                current.y = 0;
                break;
            }
            if (current.x >= map.GetLength(0))
            {
                current.x = 0;
                break;
            }
            if (current.y >= map.GetLength(1))
            {
                current.y = 0;
                break;
            }


            if (map[Convert.ToInt32(current.x), Convert.ToInt32(current.y)] != 0)
            {
                current.x -= xDirection;
                current.y -= yDirection;

                break;
            }

            xMove += xDirection;
            yMove += yDirection;
        }

        return new []{ yMove, -xMove };
    }*/
}
