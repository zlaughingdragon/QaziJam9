using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemode_Scavenger : MonoBehaviour {

    public bool gameStart, gameWin, gameFail;

    public Text timer_text, foundItems_text, notify_text;

    public float foundItems, itemsToBeFound, itemsLeft, timer, baseTime = 300;

    void Start()
    {
        timer = baseTime;
        gameStart = true;
    }

    void FixedUpdate()
    {
        if (gameStart)
        {
            timer -= Time.deltaTime;
        }
        else { timer = baseTime; }

        if(timer <= 60 && timer > 50)
        {
            notify_text.text = "One minute left! Hurry!";
        } else if(timer <= 30 && timer > 20)
        {
            notify_text.text = "30 seconds left! Hurry!";
        } else if(timer <= 10)
        {
            notify_text.text = "10 seconds left! Hurry!";
        }
        else
        {
            notify_text.text = "";
        }

        if(timer <= 0)
        {
            if(SceneManager.GetSceneByName("Loss") != null)
            SceneManager.LoadScene("Loss");
        }

    }

    void Update()
    {
        timer_text.text = "Time Left: " + timer.ToString("F0");


    }

}
