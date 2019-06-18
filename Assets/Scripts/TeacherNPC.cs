using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherNPC : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpot;

    //check if already Rotate
    private bool action;

    //rotate left right control var
    private bool leftRotated = false;

    // Start is called before the first frame update
    void Start()
    {
      waitTime = startWaitTime;
      action = false;
      randomSpot = 0;
    }

    // Update is called once per frame
    void Update()
    {
      transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

      if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f){
        if(waitTime <= 0){
            randomSpot += 1;
            if(randomSpot==moveSpots.Length){
              randomSpot = 0;
            }
            waitTime = startWaitTime;
            action = false;
        } else
          {
            if(!action)
            {
              switch (moveSpots[randomSpot].tag)
              {
                  case "rotateLeft":
                      Debug.Log("Rotate Left");
                      RotateLeft();
                      StartCoroutine(myFunction(3f));
                      action = true;
                      break;
                  case "rotateRight":
                      Debug.Log("Rotate Right");
                      RotateRight();
                      action = true;
                      break;
                  case "rotateLeftRight":
                      Debug.Log("Rotate Left then Right");
                      RotateLeft();
                      RotateRight();
                      action = true;
                      Debug.Log("finishing rotate left right...");
                      break;
                  case "skip":
                      Debug.Log("Skip");
                      Skip();
                      action = true;
                      break;
                  default:
                      Debug.Log("Default case");
                      action = true;
                      break;
              }
            }
            waitTime -= Time.deltaTime;
          }
      }
    }

    IEnumerator myFunction(float time) {
      yield return new WaitForSeconds(time);
      RotateRight();
    }

    void RotateLeft()
    {
      transform.Rotate (Vector3.forward * -90);
    }

    void RotateRight()
    {
      transform.Rotate (Vector3.forward * 90);
    }

    void RotateLeftRight()
    {
      transform.Rotate (Vector3.forward * 180);
    }

    void Skip()
    {
      waitTime = 0;
    }


}
