using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPlayerPos : MonoBehaviour{

	public float x;  // player's original x position
	public float y;  // player's original y position
	private GameObject Player;
	// private GameObject ExamBar;
	private PlayerController PlayerController;
	// private CheatBar CheatBar_English;
	// private CheatBar CheatBar_Math;
	// private CheatBar CheatBar_Science;
	public static bool inSnapPos=true;
	// public bool mathBar=true;
	// public bool scienceBar=true;
	// public bool englishBar=true;
	public List<GameObject> CheatBars;
	private List<float> CheatBarsScores;

    // Start is called before the first frame update
    void Start(){
    	Player = GameObject.Find("Player");
    	PlayerController=Player.GetComponent<PlayerController>();
			// ExamBar = GameObject.Find("cheatbar (english)");
			// CheatBar_English = ExamBar.GetComponent<CheatBar>();
			// ExamBar = GameObject.Find("cheatbar (math)");
			// CheatBar_Math = ExamBar.GetComponent<CheatBar>();
			// ExamBar = GameObject.Find("cheatbar (science)");
			// CheatBar_Science = ExamBar.GetComponent<CheatBar>();
    }

    // Update is called once per frame
    void Update(){
    }

    void OnTriggerEnter2D(Collider2D other){
			if(other.tag=="Player"|| other.tag == "Disappear"){
				StartCoroutine(Freeze(0.5f));
				other.transform.eulerAngles = new Vector3(0,0,0);
				other.transform.position = new Vector3(x, y, 0);

				// Locks the cheatbar
				var allMeetMinimumScore = true;
				foreach(GameObject CheatBar in CheatBars)
				{
					CheatBar CheatBar_Item = CheatBar.GetComponent<CheatBar>();
					CheatBar_Item.cheatBarLock(); 
					if(CheatBar_Item.size < 0.3f){
						allMeetMinimumScore = false;
					}
				}
				// CheatBar_English.cheatBarLock();
				// CheatBar_Science.cheatBarLock();
				// CheatBar_Math.cheatBarLock();

				// Immune to TeacherSight
				inSnapPos=true;

				// If above benchmark wins
				// CheatBarsScores.Clear();
				// foreach(GameObject CheatBar in CheatBars)
				// {
				// 	CheatBar CheatBar_Item = CheatBar.GetComponent<CheatBar>();
				// 	CheatBarsScores.Add(CheatBar_Item.size);
				// }
				
				// var allMeetMinimumScore = false;
				// foreach(float score in CheatBarsScores)
				// {
				// 	if(score >= 0.3f){
				// 		allMeetMinimumScore = true;
				// 	}
				// }

				if(allMeetMinimumScore){
					LevelControl.instance.youWin();
				}
				// // 1qn math
				// if(mathBar==true && scienceBar==false && englishBar==false){
				// 	if(CheatBar_Math.size >= 0.3f){
				// 		LevelControl.instance.youWin();
				// 	}
				// }
				// //1qn science
				// if(mathBar==false && scienceBar==true && englishBar==false){
				// 	if(CheatBar_Science.size>= 0.3f){
				// 		LevelControl.instance.youWin();
				// 	}
				// }
				// //1qn english
				// if(mathBar==false && scienceBar==false && englishBar==true){
				// 	if(CheatBar_English.size >= 0.3f && CheatBar_Math.size >= 0.3f && CheatBar_Science.size>= 0.3f){
				// 		LevelControl.instance.youWin();
				// 	}
				// }
				// //2qn math, science
				// if(mathBar==true && scienceBar==true && englishBar==false){
				// 	if(CheatBar_English.size >= 0.3f && CheatBar_Math.size >= 0.3f && CheatBar_Science.size>= 0.3f){
				// 		LevelControl.instance.youWin();
				// 	}
				// }
				// //2qn math, english
				// if(mathBar==true && scienceBar==false && englishBar==true){
				// 	if(CheatBar_English.size >= 0.3f && CheatBar_Math.size >= 0.3f && CheatBar_Science.size>= 0.3f){
				// 		LevelControl.instance.youWin();
				// 	}
				// }
				// //2qn science, english
				// if(mathBar==false && scienceBar==true && englishBar==true){
				// 	if(CheatBar_English.size >= 0.3f && CheatBar_Math.size >= 0.3f && CheatBar_Science.size>= 0.3f){
				// 		LevelControl.instance.youWin();
				// 	}
				// }
				// //3qns science, math, english
				// if(mathBar==true && scienceBar==true && englishBar==false){
				// 	if(CheatBar_English.size >= 0.3f && CheatBar_Math.size >= 0.3f && CheatBar_Science.size>= 0.3f){
				// 		LevelControl.instance.youWin();
				// 	}
				// }
			}
		}
	void OnTriggerExit2D(Collider2D other){
		inSnapPos=false;
	}
	IEnumerator Freeze(float time){
		PlayerController.movable = false;
		yield return new WaitForSeconds(time);
		PlayerController.movable = true;
	}
}
