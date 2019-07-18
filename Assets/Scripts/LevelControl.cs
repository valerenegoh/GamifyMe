using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance = null;
    public bool gameHasEnded = false;
    GameObject gameOverText, youWinText, endGamePanel, restartButton, nextButton;
    GameObject player;
    GameObject[] invigilators;
    int sceneIndex, levelPassed;

    void Start()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }

        gameOverText = GameObject.Find("GameOverText");
        youWinText = GameObject.Find("YouWinText");
        endGamePanel = GameObject.Find("EndGamePanel");
        restartButton = GameObject.Find("RestartButton");
        nextButton = GameObject.Find("NextButton");

        player = GameObject.Find("Player");
        invigilators = GameObject.FindGameObjectsWithTag("Invigilator");
        endGamePanel.gameObject.SetActive(false);
        gameHasEnded = false;

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
    }

    public void youWin()
    {
        if(sceneIndex==5)
        {
            Invoke("LoadMainMenu", 1f);
        }
        else
        {
            if(levelPassed < sceneIndex)
            {
                PlayerPrefs.SetInt("LevelPassed", sceneIndex);
            }
            endGamePanel.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            gameHasEnded = true;
            player.GetComponent<PlayerController>().FreezeMovement();
            foreach (GameObject invigilator in invigilators)
            {
                invigilator.GetComponent<TeacherNPC>().FreezeMovement();
            }
        }
    }

    public void youLose()
    {
        endGamePanel.gameObject.SetActive(true);
        youWinText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        gameHasEnded = true;
        player.GetComponent<PlayerController>().FreezeMovement();
        foreach (GameObject invigilator in invigilators)
            {
                invigilator.GetComponent<TeacherNPC>().FreezeMovement();
            }
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadSameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool getGameStatus()
    {
        return gameHasEnded;
    }
}
