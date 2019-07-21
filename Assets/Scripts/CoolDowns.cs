using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldowns : MonoBehaviour{
	
	public float mistCoolDownValue = 0;
	public float mistStartValue = 5;
	public Text mistTimerUI;

	// Start is called before the first frame update
	void Start(){
	}

    // Update is called once per frame
	void Update(){
		if(mistCoolDownValue >= 0){
			mistTimerUI.color = Color.magenta;
			mistTimerUI.text = mistCoolDownValue.ToString();
			mistCoolDownValue -= Time.deltaTime;
		} else{ 
			mistTimerUI.color = Color.white;
			mistTimerUI.text = "Q";
		}
		// Mist power
		if(Input.GetButton("Q") && mistCoolDownValue == 0){
			Mistify();
		}
	}
	public void Mistify(){
		print("Mist used! Cooldown started.");
		mistCoolDownValue = mistStartValue;
	}
}