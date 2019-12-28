using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinPlatformController : MonoBehaviour
{
    private const int PLAYERS_NUM = 2;
    private const float WIN_TIME = 10f;
    private const float WAIT_TIME = 1f;
    private bool[] playersOn  = new bool[PLAYERS_NUM]; // Flags to know if the players on the platform
    private string lastPlayerTag;
    private int numPlayersOn = 0;
    private float playerTimer = WIN_TIME;
    private float waitTimer = 0; 
    
    private Rigidbody[] Players = new Rigidbody[PLAYERS_NUM];
    public Rigidbody Player1;
    public Rigidbody Player2;
    
    // Start is called before the first frame update
    void Start()
    {
        // todo: Is this necessary?
        // Adding all the players to the array
        Players.Append(Player1);
        Players.Append(Player2); 
    }

    // Update is called once per frame
    void Update()
    {    
        // Update the winning timer, and check if there is a winner 
        if (numPlayersOn == 1)
        {
            if (playerTimer <= 0)
            {
                   // There is a winner
//                Rigidbody winner; // change to the class of the players
//                for (int i = 0; i < PLAYERS_NUM; i++)
//                {
//                    if (playersOn[i])
//                    {
//                        winner = Players[i];
//                    }
//                }

                Debug.Log("The winner is " + lastPlayerTag); // End game here
            }
            else
            {
                playerTimer-= Time.deltaTime;
            }
        }
        
        // Update the wait timer for the last player
        else if (numPlayersOn == 0 && waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;
        //if wait timer is on, check if its the last player and continue it's count, otherwise reset the wining timer
        if (tag.Equals("Player1")) // Player1 is on the platform 
        {
            playerEnterPlatform(tag, 0);
        }
        else if (tag.Equals("Player2")) // Player2 is on the platform 
        {
            playerEnterPlatform(tag, 1);
        }

        // Start winning timer
        if (numPlayersOn == 1)
        {
            // This the last player that have been on the platform and his wait time is still on
            if (waitTimer > 0 && tag.Equals(lastPlayerTag))
            {
                return;
            }
            // This not the last player or the wait timer is out
            playerTimer = WIN_TIME;
            lastPlayerTag = tag; //todo: Is this necessary?
        }
    }

    private void OnCollisionExit(Collision other)
    {
        string tag = other.gameObject.tag;
        if (tag.Equals("Player1")) // Player1 is on the platform 
        {
            playerExitPlatform(tag, 0);
        }
        else if (tag.Equals("Player2")) // Player2 is on the platform 
        {
            playerExitPlatform(tag, 1);
        }
    }

    private void playerEnterPlatform(string tag, int playerNum)
    {
        playersOn[playerNum] = true;
        numPlayersOn += 1;
    }
    
    private void playerExitPlatform(string playerTag, int playerNum)
    {
        playersOn[playerNum] = false;
        numPlayersOn -= 1;
        if (numPlayersOn <= 0)
        {
            lastPlayerTag = playerTag; // current player
            // Start wait timer
            waitTimer = WAIT_TIME;
        }
    }
}
