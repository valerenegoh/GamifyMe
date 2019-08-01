using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneControl : MonoBehaviour
{
    int sceneIndex;    
    private GameObject cutSceneControl;
    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("cs control's scene index: "+sceneIndex);
        cutSceneControl = GameObject.Find("CutSceneControl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadNextScene(int level){
        CutSceneControl cutSceneControlScript = cutSceneControl.GetComponent<CutSceneControl>();
        cutSceneControlScript.sceneIndex = sceneIndex;
        SceneManager.LoadScene(level);
    }
}
