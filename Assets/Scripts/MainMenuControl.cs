using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;     // for Firebase

public class MainMenuControl : MonoBehaviour{

    public Button level02Button, level03Button, level04Button;
    public Image level1, level2, level3, level4;
    public Image locklevel1, locklevel2, locklevel3, locklevel4;
    int levelPassed;
    public GameObject loginPage;
    public GameObject levelSelectionPage;
    private bool loginCheck = false;

    public InputField nameText;
    public Player player;

    public Text highScoreLvl1;
    public Text highScoreLvl2;
    public Text highScoreLvl3;
    public Text highScoreLvl4;

    public List<string> players;

    // Start is called before the first frame update
    void Start(){
        StartCoroutine(ShowStatistics());
        loginPage.SetActive(false);
        levelSelectionPage.SetActive(false);
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        level02Button.interactable = false;
        level03Button.interactable = false;
        level04Button.interactable = false;

        level1.enabled=true;
        level2.enabled=false;
        level3.enabled=false;
        level4.enabled=false;

        locklevel1.enabled=false;
        locklevel2.enabled=true;
        locklevel3.enabled=true;
        locklevel4.enabled=true;

        switch(levelPassed){
            case 2:
                level02Button.interactable = true;
                level2.enabled=true;
                locklevel2.enabled=false;
                break;
            case 4:
                level02Button.interactable = true;
                level03Button.interactable = true;
                level2.enabled=true;
                level3.enabled=true;
                locklevel2.enabled=false;
                locklevel3.enabled=false;
                break;
            case 6:
                level02Button.interactable = true;
                level03Button.interactable = true;
                level04Button.interactable = true;
                level2.enabled=true;
                level3.enabled=true;
                level4.enabled=true;
                locklevel2.enabled=false;
                locklevel3.enabled=false;
                locklevel4.enabled=false;
                break;
            case 8:
                level02Button.interactable = true;
                level03Button.interactable = true;
                level04Button.interactable = true;
                level2.enabled=true;
                level3.enabled=true;
                level4.enabled=true;
                locklevel2.enabled=false;
                locklevel3.enabled=false;
                locklevel4.enabled=false;
                break;
        }
    }

    void Update(){
      if(loginCheck){
        levelSelectionPage.SetActive(true);
        loginPage.SetActive(false);
      } else{
        loginPage.SetActive(true);
      }
    }

    public void levelToLoad (int level){
        SceneManager.LoadScene(level);
    }

    public void resetPlayerPrefs(){
        level02Button.interactable = false;
        level03Button.interactable = false;
        level04Button.interactable = false;

        level1.enabled=true;
        level2.enabled=false;
        level3.enabled=false;
        level4.enabled=false;

        locklevel1.enabled=false;
        locklevel2.enabled=true;
        locklevel3.enabled=true;
        locklevel4.enabled=true;
        PlayerPrefs.DeleteAll();
    }

    public void login(){
        loginCheck = true;
        player = new Player();
        player.setName(nameText.text);
        PlayerPrefs.SetString("Name", nameText.text);
        RestClient.Put("https://gamifyme-489ce.firebaseio.com/" + nameText.text + ".json", player);
    }

    public IEnumerator ShowStatistics(){
        int lvl1highest = 0;
        int lvl2highest = 0;
        int lvl3highest = 0;
        int lvl4highest = 0;
        string lvl1name = "";
        string lvl2name = "";
        string lvl3name = "";
        string lvl4name = "";

        foreach(string item in players){
            RestClient.Get<Player>("https://gamifyme-489ce.firebaseio.com/" + item + ".json").Then(response =>{
                player = response;
                if(player.lvl1score > lvl1highest){
                    lvl1highest = player.lvl1score;
                    lvl1name = item;
                }
                if(player.lvl2score > lvl2highest){
                    lvl2highest = player.lvl2score;
                    lvl2name = item;
                }
                if(player.lvl1score > lvl3highest){
                    lvl3highest = player.lvl3score;
                    lvl3name = item;
                }
                if(player.lvl1score > lvl4highest){
                    lvl4highest = player.lvl4score;
                    lvl4name = item;
                }
            });
            yield return new WaitForSeconds(0.5f);  //wait awhile before updating statistics
            highScoreLvl1.text = string.Format("{0} ({1} points)", lvl1name, lvl1highest.ToString());
            highScoreLvl2.text = string.Format("{0} ({1} points)", lvl2name, lvl2highest.ToString());
            highScoreLvl3.text = string.Format("{0} ({1} points)", lvl3name, lvl3highest.ToString());
            highScoreLvl4.text = string.Format("{0} ({1} points)", lvl4name, lvl4highest.ToString());
        }
    }
}
