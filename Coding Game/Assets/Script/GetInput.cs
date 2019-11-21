using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput : MonoBehaviour
{
    List<INPUTKEY> inputQueue = new List<INPUTKEY>();
    private List<INPUTKEY> UsingQueue;
    enum INPUTKEY
    {
        UP,     //0
        DOWN,   //1
        LEFT,   //2
        RIGHT   //3
    }

    private bool isMoving;
    private bool isTaskCompleted = true;
    // private Vector3 currentTargetPosition;
    
    // int[,] map = new int[5,4];
    
    // private int currentX;
    // private int currentY;

    private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        inputQueue.Clear();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isMoving)
        {
            if (!isTaskCompleted) return;
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
            /*int[] moveAmount = MoveCalc(direction);
                Debug.Log(direction);
                foreach (var item in moveAmount)
                {
                    Debug.Log(item);
                }
                currentTargetPosition = transform.position + new Vector3(moveAmount[0], moveAmount[1], 0);*/
            switch (direction)
            {
                case INPUTKEY.UP:
                    _rigidbody2D.AddForce(Time.deltaTime * 30000f * Vector2.up);
                    break;
                case INPUTKEY.DOWN:
                    _rigidbody2D.AddForce(Time.deltaTime * 30000f * Vector2.down);
                    break;
                case INPUTKEY.LEFT:
                    _rigidbody2D.AddForce(Time.deltaTime * 30000f * Vector2.left);
                    break;
                case INPUTKEY.RIGHT:
                    _rigidbody2D.AddForce(Time.deltaTime * 30000f * Vector2.right);
                    break;
            }

            isTaskCompleted = false;
            // transform.position = Vector3.Lerp(transform.position, currentTargetPosition, 10.0f * Time.deltaTime);
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
            isMoving = true;
            // currentTargetPosition = transform.position;
            UsingQueue = inputQueue;
            
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
            currentX += xDirection;
            currentY += yDirection;
            if (currentX < 0)
            {
                currentX = 0;
                break;
            }
            if (currentY < 0)
            {
                currentY = 0;
                break;
            }
            if (currentX >= map.GetLength(0))
            {
                currentX = 0;
                break;
            }
            if (currentY >= map.GetLength(1))
            {
                currentY = 0;
                break;
            }


            if (map[currentX, currentY] != 0)
            {
                currentX -= xDirection;
                currentY -= yDirection;
            
                break;
            }
            
            xMove += xDirection;
            yMove += yDirection;
        }

        return new []{ yMove, -xMove };
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter");
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        isTaskCompleted = true;
    }
}
