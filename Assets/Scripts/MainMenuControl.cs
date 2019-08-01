using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour{

    public Button level02Button, level03Button, level04Button;
    int levelPassed;

    // Start is called before the first frame update
    void Start(){
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        level02Button.interactable = false;
        level03Button.interactable = false;
        level04Button.interactable = false;

        switch(levelPassed){
            case 2:
                level02Button.interactable = true;
                break;
            case 4:
                level02Button.interactable = true;
                level03Button.interactable = true;
                break;
            case 6:
                level02Button.interactable = true;
                level03Button.interactable = true;
                level04Button.interactable = true;
                break;
            case 8:
                level02Button.interactable = true;
                level03Button.interactable = true;
                level04Button.interactable = true;
                break;
        }
    }

    public void levelToLoad (int level){
        SceneManager.LoadScene(level);
    }

    public void resetPlayerPrefs(){
        level02Button.interactable = false;
        level03Button.interactable = false;
        level04Button.interactable = false;
        PlayerPrefs.DeleteAll();
    }
}
