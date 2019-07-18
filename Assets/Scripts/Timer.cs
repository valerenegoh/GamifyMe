using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int countDownStartValue = 60;
    public Text timerUI;
    GameObject levelControl;
    private bool gameHasEnded;

    // public bool canCount = true;

    public void CountDownTimer()
    {
      gameHasEnded = levelControl.GetComponent<LevelControl>().getGameStatus();
      if(!gameHasEnded)
      {
        if (countDownStartValue >0)
      {
        // TimeSpan spanTime = TimeSpan.FromSeconds(countDownStartValue);
        timerUI.text = countDownStartValue.ToString();
        countDownStartValue--;
        Invoke("CountDownTimer", 1.0f);
      }
      else
      {
        timerUI.text = "0";
        // GameManager.instance.Result("YOU LOSE!");
        LevelControl.instance.youLose();
      }
      }
    }
    // Start is called before the first frame update
    void Start()
    {
      levelControl = GameObject.Find("LevelControl");
      CountDownTimer(); 
    }

    // public void StopTimer()
    // {
    //   canCount = false;
    // }

    // public void StartTimer()
    // {
    //   canCount = true;
    // }
}
