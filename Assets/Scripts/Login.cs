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
	private Player player;

	// Start is called before the first frame update
	void Start(){
		isNewUser = true;
	}

	void Update(){
		if(!isNewUser){
			LoadMain();
		} else{
			PlayerPrefs.SetInt("LevelPassed", 0);
			PlayerPrefs.SetInt("HighestScore2", 0);
			PlayerPrefs.SetInt("HighestScore4", 0);
			PlayerPrefs.SetInt("HighestScore6", 0);
			PlayerPrefs.SetInt("HighestScore8", 0);
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
		  });
		PlayerPrefs.SetString("Name", inputName.text);
	}

	public void LoadMain(){
		PlayerPrefs.SetInt("LevelPassed", player.latestLvl);
	    PlayerPrefs.SetInt("HighestScore2", player.lvl1score);
	    PlayerPrefs.SetInt("HighestScore4", player.lvl2score);
	    PlayerPrefs.SetInt("HighestScore6", player.lvl3score);
	    PlayerPrefs.SetInt("HighestScore8", player.lvl4score);
	    Debug.Log("LevelPassed AFTER: " + PlayerPrefs.GetInt("LevelPassed"));
		SceneManager.LoadScene(1);
	}
}
