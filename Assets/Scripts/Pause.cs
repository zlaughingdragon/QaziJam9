using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public bool paused;
    public GameObject pauseMenu, pMenu2, cam;

	void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            cam.GetComponent<MouseLook>().enabled = false;
        }
        else
        {
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
            cam.GetComponent<MouseLook>().enabled = true;
        }
    }
    public void Resume()
    {
        paused = false;
    }

    public void PauseIt()
    {
        paused = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }


    public void Options()
    {
        pauseMenu.SetActive(false);
        pMenu2.SetActive(true);
    }
    public void Option_Back()
    {
        pMenu2.SetActive(false);
        pauseMenu.SetActive(true);
        
    }
    public void Option_dofOn()
    {
        cam.GetComponent<fieldset>().dofEnabled = true;
    }
    public void Option_dofOff()
    {
        cam.GetComponent<fieldset>().dofEnabled = false;
    }

}
