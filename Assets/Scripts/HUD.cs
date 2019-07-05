using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public int countDownStartValue = 60;
    public Text timerUI;

    public RectTransform mPanelGameOver;

    public Text mTxtGameOver;

    void countDownTimer()
    {
      if (countDownStartValue >0)
      {
        // TimeSpan spanTime = TimeSpan.FromSeconds(countDownStartValue);
        timerUI.text = countDownStartValue.ToString();
        countDownStartValue--;
        Invoke("countDownTimer", 1.0f);
      }
      else
      {
        timerUI.text = "0";
        GameManager.instance.Result("YOU LOSE!");
      }
    }
    // Start is called before the first frame update
    void Start()
    {
      countDownTimer();
    }
}
