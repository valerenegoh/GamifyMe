using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class video : MonoBehaviour
{
    public double time;
    public double currentTime;
    private GameObject videoplayer; // GameObject having the attached video
    private UnityEngine.Video.VideoPlayer videofile; // VideoPLayer component 
    int sceneIndex;
    // Use this for initialization
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        videoplayer = GameObject.Find("videoplayer"); // assigning GameObject
        videofile = videoplayer.GetComponent<UnityEngine.Video.VideoPlayer>();
    }


    // Update is called once per frame
    void Update()
    {
        currentTime = videofile.time;
        if (currentTime >= time)
        {
            Debug.Log("//Change To Next Scene");
            if(sceneIndex==9){
                //it will identify if it is the last cutscene then it will load the main menu
                sceneIndex = -1;
            }
            SceneManager.LoadScene(sceneIndex + 1);
        }else
        {
            Debug.Log("Video Current Time: " + currentTime);
        }
    }
}
