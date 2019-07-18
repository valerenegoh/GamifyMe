using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlay : MonoBehaviour
{
	public bool showAtStart = true;
	public GameObject overlay;
	public AudioListener mainListener;
	// private HUD timer;
	// private TeacherNPC invigilator;

    void Awake()
	{
		// timer = FindObjectOfType<HUD>();
		// invigilator = FindObjectOfType<TeacherNPC>();
		if (showAtStart) {
			ShowLaunchScreen();
		}else {
			StartGame();
		}
	}

	public void ShowLaunchScreen()
	{	
		//disable invigilator's movement
		// invigilator.movable = false;
		mainListener.enabled = false;
		overlay.SetActive (true);
	}

    public void StartGame()
	{		
		//start timer
		// timer.countDownTimer();
		//enable invigilator's movement
		// invigilator.movable = true;
		overlay.SetActive (false);
		showAtStart = false;
		mainListener.enabled = true;
	}
}
