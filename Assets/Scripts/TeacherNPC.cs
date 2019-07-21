using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherNPC : MonoBehaviour
{
    public float speed;

    public Transform[] moveSpots;
    public int currentSpot = 0;
    public bool movable = true;
    public bool doPanning = true;
    public float turnSpeed;

    // number of tables per row
    public int numCol = 5;
    public int numRow = 3;

    // Update is called once per frame
    void Update(){ if(movable){
        if(Vector2.Distance(transform.position, moveSpots[currentSpot].position) < 0.2f){
          choseNextSpot();
        }
        Transform target = moveSpots[currentSpot];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        Vector2 dir = target.position - transform.position; 
        // transform.up = dir;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, dir), Time.fixedDeltaTime * turnSpeed);
      }
    }

    void choseNextSpot(){
      //possible spots
      // Debug.Log("current: " + currentSpot);
      List<int> possibleSpots = new List<int>();
      int total = (numRow+1)*(numCol+1);
      int up = currentSpot-numCol-1 ;
      int down = currentSpot+numCol+1;
      int left = currentSpot-1;
      int right = currentSpot+1;
      if((left+1)%(numCol+1) != 0 && left>0 || left == 0){ possibleSpots.Add(left);} 
      if (right%(numCol+1) != 0 && right<total){  possibleSpots.Add(right);}
      if(up > 0){ possibleSpots.Add(up); } 
      if(down <total){ possibleSpots.Add(down);} 
      // foreach (int i in possibleSpots){
      //   Debug.Log(i);
      // }
      currentSpot = possibleSpots[Random.Range(0, possibleSpots.Count)];
    }

    public void FreezeMovement(){
        movable = false;
    }
}
