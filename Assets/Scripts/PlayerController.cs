using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
	private Rigidbody2D player;

	public float moveSpeed;
	public float turnSpeed;
    public bool movable = true;
    public static Vector3 mySeat;

    // Start is called before the first frame update
    void Start(){
        player = GetComponent<Rigidbody2D>();
        mySeat = player.transform.position;
    }

    // Update is called once per frame
    void Update(){

    }

    void FixedUpdate(){
        if(movable){
        	float moveHorizontal = Input.GetAxis ("Horizontal");
        	float moveVertical = Input.GetAxis ("Vertical");
        	Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
            //move body
        	transform.Translate (movement * moveSpeed * Time.deltaTime, Space.World);
            //rotate body
        	if (movement != Vector2.zero) {
             transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, movement), Time.fixedDeltaTime * turnSpeed);
        	}
        }
    }

    public void FreezeMovement(){
        movable = false;
    }
}
