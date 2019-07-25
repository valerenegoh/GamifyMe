using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance = null;
    public bool gameHasEnded = false;
    private GameObject endGamePanel, restartButton, nextButton, star1, star2, star3;
    private GameObject player;
    private GameObject ExamBar;
    private CheatBar CheatBar_English;
  	private CheatBar CheatBar_Math;
  	private CheatBar CheatBar_Science;
    private GameObject[] invigilators;
    private Text gameOverText, youWinText, timerTextUI, scoreTextUI, scoreHighestTextUI, playerTitleText;
    private double score;
    private int scoreInt;
    int sceneIndex, levelPassed;

    public int optimalTimeForThirdStar = 30;
    public float optimalBarForSecondStar = 0.5f;

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

        //ui components
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        youWinText = GameObject.Find("YouWinText").GetComponent<Text>();
        timerTextUI = GameObject.Find("TimerText").GetComponent<Text>();
        playerTitleText = GameObject.Find("Title").GetComponent<Text>();
        scoreTextUI = GameObject.Find("Score").GetComponent<Text>();
        scoreHighestTextUI = GameObject.Find("HighestScore").GetComponent<Text>();
        endGamePanel = GameObject.Find("EndGamePanel");
        restartButton = GameObject.Find("RestartButton");
        nextButton = GameObject.Find("NextButton");
        star1 = GameObject.Find("Star1");
        star2 = GameObject.Find("Star2");
        star3 = GameObject.Find("Star3");
        endGamePanel.gameObject.SetActive(false);
        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);

        //players and invigilator
        player = GameObject.Find("Player");
        invigilators = GameObject.FindGameObjectsWithTag("Invigilator");

        //status
        gameHasEnded = false;

        //scenes
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelPassed = PlayerPrefs.GetInt("LevelPassed");

        //exam bars
        ExamBar = GameObject.Find("cheatbar (english)");
        CheatBar_English = ExamBar.GetComponent<CheatBar>();
        ExamBar = GameObject.Find("cheatbar (math)");
        CheatBar_Math = ExamBar.GetComponent<CheatBar>();
        ExamBar = GameObject.Find("cheatbar (science)");
        CheatBar_Science = ExamBar.GetComponent<CheatBar>();
    }

    public void youWin(){
        if(sceneIndex==5){
            Invoke("LoadMainMenu", 1f);
        }
        else{
            if(levelPassed < sceneIndex){
                PlayerPrefs.SetInt("LevelPassed", sceneIndex);
            }

            //score
            score = CheatBar_English.size*30 + CheatBar_Math.size*30 + CheatBar_Science.size*30 + int.Parse(timerTextUI.text)*0.3;
            scoreInt = (int) score;
            scoreTextUI.text = "Score: " + scoreInt;

            //highest score
            if(score>PlayerPrefs.GetInt("HighestScore"+sceneIndex)){
              youWinText.text = "NEW RECORD!";
              PlayerPrefs.SetInt("HighestScore"+sceneIndex, scoreInt);
            }

            scoreHighestTextUI.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore"+sceneIndex);

            // star scoring system
            if(CheatBar_English.size >= 0.3f && CheatBar_Math.size >= 0.3f && CheatBar_Science.size>= 0.3f){
              playerTitleText.text = "Common Cheater";
              star1.gameObject.SetActive(true);
    				}
            if(CheatBar_English.size >= optimalBarForSecondStar && CheatBar_Math.size >= optimalBarForSecondStar && CheatBar_Science.size>= optimalBarForSecondStar){
              playerTitleText.text = "Pro Cheater";
              star2.gameObject.SetActive(true);
    				}
            if(int.Parse(timerTextUI.text) >= optimalTimeForThirdStar){
              if(star2.gameObject.activeSelf){
                playerTitleText.text = "Master Cheater";
                star3.gameObject.SetActive(true);
              }
              else{
                star2.gameObject.SetActive(true);
              }
    				}

            endGamePanel.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            gameHasEnded = true;
            player.GetComponent<PlayerController>().FreezeMovement();

            foreach (GameObject invigilator in invigilators){
                invigilator.GetComponent<TeacherNPC>().FreezeMovement();
            }
        }
    }

    public void youLose(){
        //score
        scoreTextUI.text = "Score: 0";
        scoreHighestTextUI.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore"+sceneIndex);
        endGamePanel.gameObject.SetActive(true);
        youWinText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        gameHasEnded = true;
        player.GetComponent<PlayerController>().FreezeMovement();

        foreach (GameObject invigilator in invigilators){
                invigilator.GetComponent<TeacherNPC>().FreezeMovement();
            }
    }

    public void loadNextLevel(){
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void loadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void loadSameLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool getGameStatus(){
        return gameHasEnded;
    }
}
