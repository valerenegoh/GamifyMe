using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatBar : MonoBehaviour
{
	private Transform cheatBar;
	private SpriteRenderer cheatBar_renderer;
	public float size;
	private float size_lock;
	public static bool fastCopy=false;
	private Transform cheatBar_locked;
	private bool flashBool=false;
    AudioSource sound;
    private bool triggerSoundBool=false;
    // Start is called before the first frame update
    void Start()
	{
        sound = gameObject.GetComponent<AudioSource>();
        cheatBar =transform.Find("bar");
		cheatBar_renderer=cheatBar.GetComponentInChildren<SpriteRenderer>();
		cheatBar_locked=transform.Find("bar_locked");
		size=0f;
		size_lock=0f;
		cheatBar.localScale=new Vector3(size, 1f);
		cheatBar_locked.localScale=new Vector3(0f, 1f);
	}

    public void playerCheating(){
		if(size<1f){
            //playsound here
            if (!triggerSoundBool)
            {
                sound.Play();
                triggerSoundBool = true;
            }
            
            if (fastCopy){
				size=size+0.03f;
			}else{
				size=size+0.005f;
			}
		}
		cheatBar.localScale=new Vector3(size, 1f);
	}
	public void playerNotCheating(){
        //stopsound here
        if (triggerSoundBool)
        {
            sound.Stop();
            triggerSoundBool = false;
        }
        if (size>0.01f && size>size_lock){
			size=size-0.001f;
			if(!flashBool){
				StartCoroutine(flashingCheatBar());
			}
		}
		cheatBar.localScale=new Vector3(size, 1f);

	}
	private IEnumerator flashingCheatBar(){
		cheatBar_renderer.color = new Color32(160, 255, 160, 255);
		flashBool=true;
		yield return new WaitForSeconds(0.1f);
		cheatBar_renderer.color = new Color32(134, 255,59, 255);
		yield return new WaitForSeconds(0.3f);
		flashBool=false;
	}
	public void cheatBarLock(){
		size_lock=size;
		cheatBar_locked.localScale=new Vector3(size_lock, 1f);
	}
	void Update(){
		cheatBar.localScale=new Vector3(size, 1f);
	}
}
