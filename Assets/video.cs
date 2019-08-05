using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class video : MonoBehaviour
{
    public double time;
    public double currentTime;
    private GameObject videoplayer; // GameObject having the attached video
    private UnityEngine.Video.VideoPlayer videofile; // VideoPLayer component 
    // Use this for initialization
    void Start()
    {
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
        }else
        {
            Debug.Log("Video Current Time: " + currentTime);
        }
    }
}
