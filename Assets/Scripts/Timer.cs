using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

	public int countDownStartValue = 60;
	public Text timerUI;
	GameObject levelControl;
	private bool gameHasEnded;
	private AudioSource heartbeatSound;

	// Start is called before the first frame update
	void Start(){
		levelControl = GameObject.Find("LevelControl");
		heartbeatSound = GetComponent<AudioSource>();
		heartbeatSound.Stop();
		CountDownTimer();
	}

	public void CountDownTimer(){
		gameHasEnded = levelControl.GetComponent<LevelControl>().getGameStatus();
		if(!gameHasEnded){
			if (countDownStartValue > 0){
				if (countDownStartValue == 10){
					heartbeatSound.Play();
					timerUI.color = Color.red;
				}
				timerUI.text = countDownStartValue.ToString();
				countDownStartValue--;
				Invoke("CountDownTimer", 1.0f);

			} else {
				timerUI.text = "0";
				LevelControl.instance.youLose();
			}
		}
	}
}
