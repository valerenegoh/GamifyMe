using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherSight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){

    }

    void OnTriggerEnter2D (Collider2D other){
        if (other.gameObject.tag == "Player"){
            if(other.gameObject.transform.position != PlayerController.mySeat){
                LevelControl.instance.youLose();
            }
        }
    }
}
