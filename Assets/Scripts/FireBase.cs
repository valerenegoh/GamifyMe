using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class FireBase : MonoBehaviour
{

	private DatabaseReference reference;

	public Text playerScoreLvl1;
    public Text playerScoreLvl2;
    public Text playerScoreLvl3;
    public Text playerScoreLvl4;

    public Text highScoreLvl1;
    public Text highScoreLvl2;
    public Text highScoreLvl3;
    public Text highScoreLvl4;

    // Start is called before the first frame update
    void Start()
    {
    	string playerName = PlayerPrefs.GetString("Name");
        int lvlPassed = PlayerPrefs.GetInt("LevelPassed");
    	int lvl1 = PlayerPrefs.GetInt("HighestScore2");
        int lvl2 = PlayerPrefs.GetInt("HighestScore4");
        int lvl3 = PlayerPrefs.GetInt("HighestScore6");
        int lvl4 = PlayerPrefs.GetInt("HighestScore8");
        playerScoreLvl1.text = lvl1 + " points";
        playerScoreLvl2.text = lvl2 + " points";
        playerScoreLvl3.text = lvl3 + " points";
        playerScoreLvl4.text = lvl4 + " points";

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://gamifyme-489ce.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference.Child(playerName);
        
        Player player = new Player(playerName, lvl1, lvl2, lvl3, lvl4, lvlPassed);
        string json = JsonUtility.ToJson(player);
        reference.SetRawJsonValueAsync(json);
    }

    void Update(){
        StartCoroutine(ShowStatistics());
    }

    public IEnumerator ShowStatistics(){
    	List<Player> lvl1scores = new List<Player>();
    	List<Player> lvl2scores = new List<Player>();
    	List<Player> lvl3scores = new List<Player>();
    	List<Player> lvl4scores = new List<Player>();
        int lvl1highest = 0;
        int lvl2highest = 0;
        int lvl3highest = 0;
        int lvl4highest = 0;
        string lvl1name = "";
        string lvl2name = "";
        string lvl3name = "";
        string lvl4name = "";

        yield return new WaitForSeconds(0.5f);
        FirebaseDatabase.DefaultInstance.GetReferenceFromUrl("https://gamifyme-489ce.firebaseio.com/")
            .OrderByChild("lvl1score").LimitToLast(10).GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted){
	                Debug.Log("Fail To Load");
	            }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    foreach(var child in snapshot.Children){
                        string json = child.GetRawJsonValue();
                        Player item = JsonUtility.FromJson<Player>(json);
                        lvl1scores.Add(item);
                    }
                    lvl1scores.Reverse();
                }
          });
        FirebaseDatabase.DefaultInstance.GetReferenceFromUrl("https://gamifyme-489ce.firebaseio.com/")
            .OrderByChild("lvl2score").LimitToLast(10).GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted){
	                Debug.Log("Fail To Load");
	            }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    foreach(var child in snapshot.Children){
                        string json = child.GetRawJsonValue();
                        Player item = JsonUtility.FromJson<Player>(json);
                        lvl2scores.Add(item);
                    }
                    lvl2scores.Reverse();
                }
          });
        FirebaseDatabase.DefaultInstance.GetReferenceFromUrl("https://gamifyme-489ce.firebaseio.com/")
            .OrderByChild("lvl3score").LimitToLast(10).GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted){
	                Debug.Log("Fail To Load");
	            }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    foreach(var child in snapshot.Children){
                        string json = child.GetRawJsonValue();
                        Player item = JsonUtility.FromJson<Player>(json);
                        lvl3scores.Add(item);
                    }
                    lvl3scores.Reverse();
                }
          });
        FirebaseDatabase.DefaultInstance.GetReferenceFromUrl("https://gamifyme-489ce.firebaseio.com/")
            .OrderByChild("lvl4score").LimitToLast(10).GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted){
	                Debug.Log("Fail To Load");
	            }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    foreach(var child in snapshot.Children){
                        string json = child.GetRawJsonValue();
                        Player item = JsonUtility.FromJson<Player>(json);
                        lvl4scores.Add(item);
                    }
                    lvl4scores.Reverse();
                }
          });
        yield return new WaitForSeconds(0.5f);
        string lvl1text = "";
        foreach(Player item in lvl1scores){
        	lvl1text += item.name + " (" + item.lvl1score + " points)\n";
        }
        highScoreLvl1.text = lvl1text;

        string lvl2text = "";
        foreach(Player item in lvl2scores){
        	lvl2text += item.name + " (" + item.lvl2score + " points)\n";
        }
        highScoreLvl2.text = lvl2text;

        string lvl3text = "";
        foreach(Player item in lvl3scores){
        	lvl3text += item.name + " (" + item.lvl3score + " points)\n";
        }
        highScoreLvl3.text = lvl3text;

        string lvl4text = "";
        foreach(Player item in lvl4scores){
        	lvl4text += item.name + " (" + item.lvl4score + " points)\n";
        }
        highScoreLvl4.text = lvl4text;
    }

    public void back(){
        Debug.Log("pressed");
        SceneManager.LoadScene(0);
    }
}
