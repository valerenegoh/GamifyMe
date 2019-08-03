using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
      public float barHeight = 0.0f;
      public float barY = -6.4f;
      public float barX = -2f;
  
      void Update() {
          Vector3 pos = player.transform.position;
          pos.z += barHeight;
          pos.y += barY;
          pos.x += barX;
          transform.position = pos;
      }
}
