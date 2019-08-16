using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour{

	public InputField inputName;
	private DatabaseReference reference;
	public bool isNewUser;
	private bool clicked;
	private Player player;

	// Start is called before the first frame update
	void Start(){
		isNewUser = true;
	}

	void Update(){
		if(!isNewUser && clicked){
			PlayerPrefs.SetInt("LevelPassed", player.latestLvl);
		    PlayerPrefs.SetInt("HighestScore3", player.lvl1score);
		    PlayerPrefs.SetInt("HighestScore5", player.lvl2score);
		    PlayerPrefs.SetInt("HighestScore7", player.lvl3score);
		    PlayerPrefs.SetInt("HighestScore9", player.lvl4score);
			SceneManager.LoadScene(1);
		} else if(isNewUser && clicked){
			PlayerPrefs.SetInt("LevelPassed", 0);
			PlayerPrefs.SetInt("HighestScore3", 0);
			PlayerPrefs.SetInt("HighestScore5", 0);
			PlayerPrefs.SetInt("HighestScore7", 0);
			PlayerPrefs.SetInt("HighestScore9", 0);
			SceneManager.LoadScene(1);
		}
	}

	public void login(){
		FirebaseDatabase.DefaultInstance.GetReferenceFromUrl("https://gamifyme-489ce.firebaseio.com/")
			.GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted){
				 Debug.Log("Fail To Load");
			 	}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					if(snapshot.HasChild(inputName.text)){
					  // setPlayerPrefs  
					  string json = snapshot.Child(inputName.text).GetRawJsonValue();
                      player = JsonUtility.FromJson<Player>(json);
                      isNewUser = false;
					}
				}
				clicked = true;
		  });
		PlayerPrefs.SetString("Name", inputName.text);
	}
}
