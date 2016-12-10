using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour
{
	public string sceneName = "Game";

	public void OnClick()
	{
		SceneManager.LoadScene(sceneName);
	}
}
