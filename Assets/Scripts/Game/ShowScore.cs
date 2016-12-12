using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowScore : MonoBehaviour {

    public Text score_txt;
    public float score;
    public int completed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        score = PlayerPrefs.GetFloat("Score");
        completed = PlayerPrefs.GetInt("Completed");
        score_txt.text = "Your Score: " + score.ToString("F2");
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Share()
    {
        System.Diagnostics.Process.Start("http://www.dragonmirth.com/sharestats.html");
    }

}
