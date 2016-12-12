using System;

using System.Collections;
using System.Linq;
using System.Collections.Generic;

using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Gamemode_Scavenger : MonoBehaviour {

    public bool gameStart, gameWin, gameFail, pickup;

    public Text timer_text, foundItems_text, notify_text;

    public float foundItems, itemsToBeFound, itemsLeft, timer, baseTime = 300, score = 0;

    

    void Start()
    {
        gameStart = true;
        timer = baseTime;
        itemsToBeFound = GameObject.FindGameObjectsWithTag("item").Length;
    }

    void FixedUpdate()
    {
        itemsLeft = itemsToBeFound - foundItems;
        if (gameStart)
        {
            timer -= Time.deltaTime;
        }
        else { timer = baseTime; }

        if(timer < baseTime && timer > (baseTime - 10))
        {
            notify_text.text = "Use the 'E' key to pick things up, or left click.";
        }

        else if(timer <= 60 && timer > 50)
        {
            notify_text.text = "One minute left! Hurry!";
        } else if(timer <= 30 && timer > 20)
        {
            notify_text.text = "30 seconds left! Hurry!";
        } else if(timer <= 10 && timer > 4)
        {
            notify_text.text = "10 seconds left! Hurry!";
        }
        else
        {
            notify_text.text = "";
        }

        if(timer <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            timer = 0;
            gameFail = true;
        }





        score = foundItems * timer;


        if(foundItems >= itemsToBeFound && timer > 0.01f)
        {
            gameWin = true;
        }

        if (gameWin)
        {
            PlayerPrefs.SetFloat("AchievedTime", timer);
            PlayerPrefs.SetFloat("Collected Items", foundItems);
            PlayerPrefs.SetFloat("Score", timer * foundItems);
            PlayerPrefs.SetInt("Completed", 1);
            SceneManager.LoadScene("Win");
        }
        else if (gameFail)
        {
            PlayerPrefs.SetFloat("AchievedTime", 0);
            PlayerPrefs.SetFloat("Collected Items", foundItems);
            PlayerPrefs.SetInt("Completed", 1);
            SceneManager.LoadScene("Loss");
        } 

    }
    GameObject tmp;
    void Update()
    {
        timer_text.text = "Time Left: " + timer.ToString("F0");
        foundItems_text.text = "Items: " + foundItems.ToString("F0") + "/" + itemsToBeFound.ToString("F0");

        if (Input.GetButtonDown("Pickup") && pickup)
        {
            foundItems++;
            Destroy(tmp);
            pickup = false;
            tmp = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("item"))
        {
            print("found item " + other.gameObject.name);
            pickup = true;
            tmp = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("item"))
        {
            
            pickup = false;
            tmp = null;
        }
    }

}
