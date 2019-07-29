using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{
	private Rigidbody2D player;
    private Animator anim;

	public float moveSpeed = 3;
	public float turnSpeed = 100;
    public bool movable = true;
    public static Vector3 mySeat;  // for teacher to know not to catch

    public float mistCoolDownValue = 10;
    public bool mistIsCooling;
    public Image mistCooldownImg;

    public bool isDashing;
    public float dashCoolDownValue = 5;
    public bool dashIsCooling;
    public Image dashCooldownImg;

    public float fcopyCoolDownValue = 15;
    public bool fcopyIsCooling;
    public Image fcopyCooldownImg;

    // Start is called before the first frame update
    void Start(){
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        mySeat = player.transform.position;
        mistCooldownImg.fillAmount = 0;
        dashCooldownImg.fillAmount = 0;
        fcopyCooldownImg.fillAmount = 0;
    }

    // Update is called once per frame
    void Update(){
       if(!movable){
            anim.SetBool("isDashing", false);
            anim.SetBool("isWalking", false);
        }   
        if(movable){
            if(!isDashing){
                anim.SetBool("isDashing", false);
            }

            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");

            // set animation to idle
            if(moveHorizontal == 0 && moveVertical == 0){
                if(isDashing){
                    anim.SetBool("isDashing", false);
                } else{
                    anim.SetBool("isWalking", false);
                }
            } else{
                if(isDashing){
                    anim.SetBool("isDashing", true);
                } else{
                    anim.SetBool("isWalking", true);
                }
            }

            Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
            //move body
            transform.Translate (movement * moveSpeed * Time.deltaTime, Space.World);
            //rotate body
            if (movement != Vector2.zero) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, movement), Time.fixedDeltaTime * turnSpeed);
            }
        }

        // Mist power
        if(Input.GetKeyDown(KeyCode.Q)){
            if(!mistIsCooling && !isDashing && !isCopying){
                StartCoroutine(Mistify());
            }  
        }
        if(mistIsCooling){
            mistCooldownImg.fillAmount += 1/mistCoolDownValue * Time.deltaTime;
            if(mistCooldownImg.fillAmount >= 1){
                mistCooldownImg.fillAmount = 0;
                mistIsCooling = false;
            }
        }

        // Dash power
        if(Input.GetKeyDown(KeyCode.W)){
            if(!dashIsCooling && !isMisting && !isCopying){
                StartCoroutine(Dash());
            }  
        }
        if(dashIsCooling){
            dashCooldownImg.fillAmount += 1/dashCoolDownValue * Time.deltaTime;
            if(dashCooldownImg.fillAmount >= 1){
                dashCooldownImg.fillAmount = 0;
                dashIsCooling = false;
            }
        }

        // Fast Copy power
        if(Input.GetKeyDown(KeyCode.E) && !isMisting && !isDashing){
            if(!fcopyIsCooling && !isMisting && !isDashing){
                StartCoroutine(FastCopy());
            }  
        }
        if(fcopyIsCooling){
            fcopyCooldownImg.fillAmount += 1/fcopyCoolDownValue * Time.deltaTime;
            if(fcopyCooldownImg.fillAmount >= 1){
                fcopyCooldownImg.fillAmount = 0;
                fcopyIsCooling = false;
            }
        }
    }

    public void FreezeMovement(){
        movable = false;
    }

    public IEnumerator Mistify(){
        mistIsCooling = true;
        player.tag = "Disappear";
        anim.SetBool("isMisting", true);
        yield return new WaitForSeconds(3f);
        player.tag = "Player";
        anim.SetBool("isMisting", false);
    }

    public IEnumerator Dash(){
        dashIsCooling = true;
        moveSpeed = 5;
        isDashing = true;
        yield return new WaitForSeconds(3f);
        moveSpeed = 3;
        isDashing = false;
    }

    public IEnumerator FastCopy(){
        fcopyIsCooling = true;
		CheatBar.fastCopy=true;
        anim.SetBool("isCopying", true);
        yield return new WaitForSeconds(3f);
		CheatBar.fastCopy=false;
        anim.SetBool("isCopying", false);
    }
}
