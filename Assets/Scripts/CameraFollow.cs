using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour {
      public GameObject player;
      public float cameraHeight = -10.0f;
  
      void Update() {
          Vector3 pos = player.transform.position;
          pos.z += cameraHeight;
          transform.position = pos;
      }
  }
