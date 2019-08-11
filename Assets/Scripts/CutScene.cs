using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour{
    private Animator anim;

    public bool mistIsDone = true;
    private AudioSource mistAudio;

    public bool dashIsDone = true;
    private AudioSource dashAudio; 

    public bool fcopyIsDone = true;
    private AudioSource fcopyAudio;  

    // Start is called before the first frame update
    void Start(){
        anim = GetComponent<Animator>();

        AudioSource[] audios = GetComponents<AudioSource>();
        mistAudio = audios[0];
        dashAudio = audios[1];
        fcopyAudio = audios[2];
    }

    // Update is called once per frame
    void Update(){
        // Mist power
        if(Input.GetKeyDown(KeyCode.Q)){
            if(mistIsDone){
                StartCoroutine(Mistify());
            }  
        }

        // Dash power
        if(Input.GetKeyDown(KeyCode.W)){
            if(dashIsDone){
                StartCoroutine(Dash());
            }  
        }

        // Fast Copy power
        if(Input.GetKeyDown(KeyCode.E)){
            if(fcopyIsDone){
                StartCoroutine(FastCopy());
            }  
        }
    }

    public IEnumerator Mistify(){
        anim.SetBool("isMisting", true);
        mistAudio.Play();
        mistIsDone = false;
        yield return new WaitForSeconds(3f);
        mistIsDone = true;
        anim.SetBool("isMisting", false);
    }

    public IEnumerator Dash(){
        anim.SetBool("isDashing", true);
        dashAudio.Play();
        dashIsDone = false;
        yield return new WaitForSeconds(3f);
        dashIsDone = true;
        anim.SetBool("isDashing", false);
    }

    public IEnumerator FastCopy(){
        anim.SetBool("isCopying", true);
        fcopyAudio.Play();
        fcopyIsDone = false;
		CheatBar.fastCopy=true;
        yield return new WaitForSeconds(3f);
        fcopyIsDone = true;
		CheatBar.fastCopy=false;
        anim.SetBool("isCopying", false);
    }
}