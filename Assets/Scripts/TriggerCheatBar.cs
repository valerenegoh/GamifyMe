using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheatBar : MonoBehaviour
{
	private GameObject cheatBar;
	private CheatBar cheatBarScript;
	public string cheatBarName;
	void Start(){
		cheatBar= GameObject.Find(cheatBarName);
		cheatBarScript=cheatBar.GetComponent<CheatBar>();
	}
	void OnTriggerEnter2D(Collider2D other){
		
		if(other.tag=="Player"){
			cheatBarScript.playerCheating();
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag=="Player"){
			cheatBarScript.playerNotCheating();
		}
	}

}
