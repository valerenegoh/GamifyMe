using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPlayerPos : MonoBehaviour{

	public float x;  // player's original x position
	public float y;  // player's original y position
	private GameObject Player;
	private AudioSource barAudio;
	// private bool firstSeating = true;
	private Dictionary<int, float> bars;
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
	public Sprite sittingImg;

    // Start is called before the first frame update
    void Start(){
    	Player = GameObject.Find("Player");
    	PlayerController=Player.GetComponent<PlayerController>();
			barAudio = GetComponent<AudioSource>();
			barAudio.Stop();
			bars = new Dictionary<int, float>();
			bars.Add(0, 0.0f);
			bars.Add(1, 0.0f);
			bars.Add(2, 0.0f);
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
				//sit down
				if (other.transform.childCount > 0){
					other.transform.GetChild(0).gameObject.SetActive(false);
					other.GetComponent<SpriteRenderer>().sprite = sittingImg;
				}

				StartCoroutine(Freeze(0.5f));
				other.transform.eulerAngles = new Vector3(0,0,0);
				other.transform.position = new Vector3(x, y, 0);

				// Locks the cheatbar
				var allMeetMinimumScore = true;
				// if(!firstSeating){
				// 	barAudio.Play();
				// }
				var counter = 0;
				foreach(GameObject CheatBar in CheatBars)
				{
					CheatBar CheatBar_Item = CheatBar.GetComponent<CheatBar>();
					CheatBar_Item.cheatBarLock();
					if(bars[counter]<CheatBar_Item.size){
						//update temp bar value
						barAudio.Play();
						bars[counter] = CheatBar_Item.size;
					}
					if(CheatBar_Item.size < 0.3f){
						allMeetMinimumScore = false;
					}
					counter+=1;
				}
				inSnapPos=true;
				if(allMeetMinimumScore){
					LevelControl.instance.youWin();
				}
			}
		}
	void OnTriggerExit2D(Collider2D other){
		// stand up
		if (other.transform.childCount > 0){
			other.GetComponent<SpriteRenderer>().sprite = null;
			other.transform.GetChild(0).gameObject.SetActive(true);
		}

		inSnapPos=false;
	}
	IEnumerator Freeze(float time){
		PlayerController.movable = false;
		yield return new WaitForSeconds(time);
		PlayerController.movable = true;
	}
}