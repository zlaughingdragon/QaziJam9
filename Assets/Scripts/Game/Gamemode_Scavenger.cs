using System;

using System.Collections;
using System.Linq;
using System.Collections.Generic;

using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Gamemode_Scavenger : MonoBehaviour {

    public bool gameStart, gameWin, gameFail, pickup, stamExhaust, canRegen, regen;

    public Text timer_text, foundItems_text, notify_text;
    public Image timer_radial, timer_solid, item_radial, item_solid, stam_radial, stam_solid;

    public float foundItems, itemsToBeFound, itemsLeft, timer, baseTime = 300, score = 0, stamina = 100, maxStamina = 100;

    float stamprev, stamnext;

    void Start()
    {
        gameStart = true;
        timer = baseTime;
        itemsToBeFound = GameObject.FindGameObjectsWithTag("item").Length;
    }

    void FixedUpdate()
    {
        if(stamina < 0)
        {
            stamina = 0;
        }
        if (stamExhaust)
        {
            
            stamina -= Time.deltaTime * 3;
            
        }
        else if(!regen && stamina < 100)
        {
            StartCoroutine("Reguvinate");
        }

        if (canRegen && stamExhaust == false)
        {
            stamina += Time.deltaTime;
        }
        if (stamina >= 100)
        {
            stamina = 100;
            canRegen = false;
        }
        stam_radial.fillAmount = stamina / maxStamina;

            itemsLeft = itemsToBeFound - foundItems;
        if (gameStart)
        {
            timer_radial.color = new Color(0, 1, 0);
            timer -= Time.deltaTime;
        }
        else { timer = baseTime; }

        if(timer < baseTime && timer > (baseTime - 10))
        {
            timer_radial.color = Color.Lerp(timer_radial.color, new Color(0,0,7), .7f);
            notify_text.text = "Use the 'E' key to pick things up, or left click.";
        }

        else if(timer <= 60 && timer > 50)
        {
            timer_radial.color = Color.Lerp(timer_radial.color, new Color(7, 1f, 0), .7f);
            notify_text.text = "One minute left! Hurry!";
        } else if(timer <= 30 && timer > 20)
        {
            timer_radial.color = Color.Lerp(timer_radial.color, new Color(7, 0f, 0), .7f);
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

        

        timer_radial.fillAmount = timer / baseTime;
        
        
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

    IEnumerator Reguvinate()
    {
        regen = true;
        yield return new WaitForSeconds(1);
        canRegen = true;
        regen = false;
    }

    GameObject tmp;
    void Update()
    {
        timer_text.text = timer.ToString("F0");
        foundItems_text.text = foundItems.ToString("F0") + "/" + itemsToBeFound.ToString("F0");

        if (Input.GetButtonDown("Pickup") && pickup)
        {
            foundItems++;
            stamina += 10f;
            Destroy(tmp);
            pickup = false;
            tmp = null;
        }
        item_radial.fillAmount = foundItems / itemsToBeFound;
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
