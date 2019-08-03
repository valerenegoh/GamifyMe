using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherSight : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other){
        if (other.gameObject.tag == "Player"){
            if(other.gameObject.transform.position != PlayerController.mySeat){
                if(!SnapPlayerPos.inSnapPos){
              	    LevelControl.instance.youLose();
                }
            }
        }
    }
}
