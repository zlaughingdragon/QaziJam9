using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour
{
	public string sceneName = "Game";

    public void Play()
    {
        SceneManager.LoadScene(sceneName);
    }

	public void Quit()
	{
        Application.Quit();
	}
}
