using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public GameObject youWinText;
    public GameObject youWinPanel;
    public static GameManager instance = null;

    void Awake()
    {
      if (instance == null)
      {
        instance = this;
      }
      else if (instance != null)
      {
        Destroy (gameObject);
      }
    }

    public void EndGame ()
    {
      if (gameHasEnded == false)
      {
        gameHasEnded = true;
        Debug.Log("Game OVER");
        Invoke("Restart", restartDelay);
      }
    }

    public void Result(String result)
    {
      youWinText.GetComponent<Text>().text = result;
      youWinText.SetActive(true);
      youWinPanel.SetActive(true);
      Invoke("Restart", restartDelay);
    }

    void Restart ()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
