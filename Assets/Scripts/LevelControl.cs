using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance = null;
    public bool gameHasEnded = false;
    AudioSource loseSound;
    AudioSource winSound;

    private GameObject endGamePanel, restartButton, nextButton, star1, star2, star3;

    public List<GameObject> CheatBars;
    private GameObject[] invigilators;
    private Text gameOverText, youWinText, timerTextUI, scoreTextUI, scoreHighestTextUI, playerTitleText;
    private Image youWinImage, gameOverImage;
    private double score;
    private int scoreInt;
    public int sceneIndex, levelPassed;

    public int optimalTimeForThirdStar = 30;
    public float optimalBarForSecondStar = 0.5f;

    private GameObject player;

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
        AudioSource[] audios = GetComponents<AudioSource>();
        loseSound = audios[0];
        winSound = audios[1];
        loseSound.Stop();
        winSound.Stop();

        //ui components
        gameOverImage = GameObject.Find("GameOverImage").GetComponent<Image>();
        youWinImage = GameObject.Find("YouWinImage").GetComponent<Image>();
        // gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        // youWinText = GameObject.Find("YouWinText").GetComponent<Text>();
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
        Debug.Log("level control's scene index: "+sceneIndex);
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
    }

    void Update(){
      if (Input.GetKeyDown("r")){
        youLose();
      }
    }

    public void youWin(){

        winSound.Play();
        if(levelPassed < sceneIndex){
            PlayerPrefs.SetInt("LevelPassed", sceneIndex);
        }

        //score
        float barScoresTotal = 0;
        int numberOfBars = 0;
        var firstStarCheckMeet = true;
        var secondStarCheckMeet = true;
        foreach(GameObject CheatBar in CheatBars)
        {
          numberOfBars+=1;
        }
        foreach(GameObject CheatBar in CheatBars)
        {
          CheatBar CheatBar_Item = CheatBar.GetComponent<CheatBar>();
          Debug.Log(CheatBar_Item.size);
          barScoresTotal += CheatBar_Item.size*(90/numberOfBars);
          if(CheatBar_Item.size < 0.3f){
            firstStarCheckMeet = false;
          }
          if(CheatBar_Item.size < optimalBarForSecondStar){
            secondStarCheckMeet = false;
          }
        }
        score = barScoresTotal + int.Parse(timerTextUI.text)*0.3;
        scoreInt = (int) score;
        scoreTextUI.text = "Score: " + scoreInt;

        //highest score
        if(score>PlayerPrefs.GetInt("HighestScore" + sceneIndex)){
            PlayerPrefs.SetInt("HighestScore" + sceneIndex, scoreInt);
          // youWinText.text = "NEW RECORD!";
        }
        scoreHighestTextUI.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore"+sceneIndex);

        // star scoring system
        if(firstStarCheckMeet){
          playerTitleText.text = "Common Cheater";
          star1.gameObject.SetActive(true);
        }
        if(secondStarCheckMeet){
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
        gameOverImage.enabled = false;
        // gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        gameHasEnded = true;
        player.GetComponent<PlayerController>().FreezeMovement();

        foreach (GameObject invigilator in invigilators){
            invigilator.GetComponent<TeacherNPC>().FreezeMovement();
        }
    }

    public void youLose(){
        //score
        loseSound.Play();
        scoreTextUI.text = "Score: 0";
        scoreHighestTextUI.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore"+sceneIndex);
        endGamePanel.gameObject.SetActive(true);
        youWinImage.enabled = false;
        // youWinText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        gameHasEnded = true;
        player.GetComponent<PlayerController>().FreezeMovement();

        foreach (GameObject invigilator in invigilators){
          if(invigilator.GetComponent<TeacherNPC>()){
            invigilator.GetComponent<TeacherNPC>().FreezeMovement();
          }
          if(invigilator.GetComponent<TeacherRotation>()){
            invigilator.GetComponent<TeacherRotation>().FreezeMovement();
          }
        }
    }

    public void loadNextLevel(int level){
        SceneManager.LoadScene(level);
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
