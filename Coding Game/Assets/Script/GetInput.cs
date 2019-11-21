﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetInput : MonoBehaviour
{
    public AudioClip coinSound;
    public AudioClip keySound;
    private AudioSource audioSource;

    public float speed = 30000f;
    
    List<INPUTKEY> inputQueue = new List<INPUTKEY>();
    private List<INPUTKEY> UsingQueue;
    public enum INPUTKEY
    {
        UP,     //0
        DOWN,   //1
        LEFT,   //2
        RIGHT   //3
    }

    private bool isMoving;
    private bool isTaskCompleted = true;

    public int mapCoins = 2;
    private int coins = 0;

    private Rigidbody2D _rigidbody2D;
    GameObject player;
    GameObject coinText;

    List<int> loc = new List<int>();

    private UIController uiController;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        inputQueue.Clear();
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
        coinText = GameObject.Find("Coin Text");
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];

        coinText.GetComponent<TextMeshProUGUI>().text = ("Coins : " + coins + " / " + mapCoins);
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(player.transform.rotation.z);
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
                spriteRenderer.sprite = sprites[0];
                return;
            }
            switch (direction)
            {
                case INPUTKEY.UP:
                    _rigidbody2D.AddForce(Time.deltaTime * speed * Vector2.up);
                    player.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case INPUTKEY.DOWN:
                    _rigidbody2D.AddForce(Time.deltaTime * speed * Vector2.down);
                    player.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case INPUTKEY.LEFT:
                    _rigidbody2D.AddForce(Time.deltaTime * speed * Vector2.left);
                    player.transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case INPUTKEY.RIGHT:
                    _rigidbody2D.AddForce(Time.deltaTime * speed * Vector2.right);
                    player.transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
            }

            isTaskCompleted = false;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            uiController.AddCommand(INPUTKEY.UP);
            inputQueue.Add(INPUTKEY.UP);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            uiController.AddCommand(INPUTKEY.DOWN);
            inputQueue.Add(INPUTKEY.DOWN);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            uiController.AddCommand(INPUTKEY.LEFT);
            inputQueue.Add(INPUTKEY.LEFT);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            uiController.AddCommand(INPUTKEY.RIGHT);
            inputQueue.Add(INPUTKEY.RIGHT);
        }
        
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            try
            {
                uiController.RemoveCommand();
                inputQueue.RemoveAt(inputQueue.Count - 1);
            }
            catch (Exception ignored) {}
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true;
            spriteRenderer.sprite = sprites[1];
            // currentTargetPosition = transform.position;
            UsingQueue = inputQueue;
            
            string output = "";

            foreach (int i in inputQueue)
            {
                
                output = output + i + " ";
            }

            Debug.Log(output);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(keySound);
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

                if (loc[0] > Mathf.RoundToInt(player.transform.position.x))
                {
                    loc.Add(loc[0] - 1);
                }
                else if (loc[0] < Mathf.RoundToInt(player.transform.position.x))
                {
                    loc.Add(loc[0] + 1);
                }
                else
                {
                    loc.Add(Mathf.RoundToInt(player.transform.position.x));
                }

                if (loc[1] > Mathf.RoundToInt(player.transform.position.y))
                {
                    loc.Add(loc[1] - 1);
                }
                else if (loc[1] < Mathf.RoundToInt(player.transform.position.y))
                {
                    loc.Add(loc[1] + 1);
                }
                else
                {
                    loc.Add(Mathf.RoundToInt(player.transform.position.y));
                }

                Debug.Log(player.transform.position.x + " " + player.transform.position.y + " to");
                Debug.Log(loc[2] + " " + loc[3]);
                player.transform.position = new Vector2(loc[2], loc[3]);
                loc.RemoveRange(0, 4);

                break;

            case "coin":
                Destroy(other.gameObject);
                coins++;
                audioSource.PlayOneShot(coinSound);
                
                coinText.GetComponent<TextMeshProUGUI>().text = ("Coins : " + coins + " / " + mapCoins);

                break;
        }
    }
}
