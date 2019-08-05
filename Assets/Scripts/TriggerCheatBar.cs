using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TriggerCheatBar : MonoBehaviour
{
	//Cheat Bar
	private GameObject cheatBar;
	private CheatBar cheatBarScript;
	public string cheatBarName;
	private bool cheatingRange;
	float timerDelay;
	int loop;

	//Cheating Animation
	[SerializeField] private Transform pfCheatingText;
	public List<string> cheatingPopupString;


	void Start(){
		cheatBar= GameObject.Find(cheatBarName);
		cheatBarScript=cheatBar.GetComponent<CheatBar>();
		timerDelay=0f;
		loop=0;
	}
	void OnTriggerEnter2D(Collider2D other){
		
		if(other.tag=="Player" || other.tag== "Disappear")
        {
        
			cheatingRange=true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag=="Player" || other.tag == "Disappear")
        {
			cheatingRange=false;
		}
	}

	void CreateCheatPopup(string cheatText){
		bool isCriticalHit=Random.Range(0,100)<30;
		Vector3 position=transform.position;
		position.y+=0.8f;
		position.x+=0.2f;

		Transform cheatingPopupTransform=Instantiate(pfCheatingText, position, Quaternion.identity);
		CheatingPopup cheatingPopup= cheatingPopupTransform.GetComponent<CheatingPopup>();
		cheatingPopup.Setup(cheatText, isCriticalHit);
	}

	void Update(){
		timerDelay+=Time.deltaTime;
		if(cheatingRange && (Input.GetKey("space"))){
			// Cheating
			cheatBarScript.playerCheating();
			// Put cheating animation here
			string cheatingText="";
			if (timerDelay>1.5f){
				cheatingText=cheatingPopupString[loop];
				if(loop<cheatingPopupString.Count-1){
					loop++;
				}
				else{
					loop=0;
				}
				CreateCheatPopup(cheatingText);
				timerDelay=0f;
			}
		}
		else{
			// NotCheating 
			cheatBarScript.playerNotCheating();

		}
	}

}
