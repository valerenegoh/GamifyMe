using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlay : MonoBehaviour
{
	public bool showAtStart = true;
	public GameObject overlay;
	public AudioListener mainListener;

    void Awake()
	{
		if (showAtStart) {
			ShowLaunchScreen();
		}else {
			StartGame();
		}
	}

	public void ShowLaunchScreen()
	{
		mainListener.enabled = false;
		overlay.SetActive (true);
	}

    public void StartGame()
	{		
		overlay.SetActive (false);
		showAtStart = false;
		mainListener.enabled = true;
	}
}
