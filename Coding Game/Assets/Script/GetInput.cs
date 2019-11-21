using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput : MonoBehaviour
{
    public AudioClip coinSound;
    private AudioSource audioSource;
    
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

    public int coins = 0;

    private Rigidbody2D _rigidbody2D;
    GameObject player;

    List<int> loc = new List<int>();

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        inputQueue.Clear();
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "water":
                Debug.Log("Trigger Enter");
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0;
                isTaskCompleted = true;

                loc.Add((int)other.transform.position.x);
                loc.Add((int)other.transform.position.y);

                if (loc[0] > (int)player.transform.position.x)
                {
                    loc.Add(loc[0] - 1);
                }
                else if (loc[0] < (int)player.transform.position.x)
                {
                    loc.Add(loc[0] + 1);
                }
                else
                {
                    loc.Add((int)player.transform.position.x);
                }

                if (loc[1] > (int)player.transform.position.y)
                {
                    loc.Add(loc[1] - 1);
                }
                else if (loc[1] < (int)player.transform.position.y)
                {
                    loc.Add(loc[1] + 1);
                }
                else
                {
                    loc.Add((int)player.transform.position.y);
                }

                Debug.Log(loc[2] + " " + loc[3]);
                player.transform.position = new Vector2(loc[2], loc[3]);
                loc.RemoveRange(0, 4);

                break;

            case "coin":
                coins++;
                audioSource.PlayOneShot(coinSound);
                Destroy(other.gameObject);

                break;
        }
    }
}
