using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatBar : MonoBehaviour
{
	private Transform cheatBar;
	public float size;
	private float size_lock;
	public static bool fastCopy=false;
	private Transform cheatBar_locked;

	// Start is called before the first frame update
	void Start()
	{
		cheatBar=transform.Find("bar");
		cheatBar_locked=transform.Find("bar_locked");
		size=0f;
		size_lock=0f;
		cheatBar.localScale=new Vector3(size, 1f);
		cheatBar_locked.localScale=new Vector3(0f, 1f);
	}

	public void playerCheating(){
		if(size<1f){
			if(fastCopy){
				size=size+0.03f;
			}else{
				size=size+0.005f;
			}
		}
		cheatBar.localScale=new Vector3(size, 1f);
	}
	public void playerNotCheating(){
		if(size>0.01f && size>size_lock){
			size=size-0.001f;
		}
		cheatBar.localScale=new Vector3(size, 1f);
	}
	public void cheatBarLock(){
		size_lock=size;
		cheatBar_locked.localScale=new Vector3(size_lock, 1f);
	}
	void Update(){
		cheatBar.localScale=new Vector3(size, 1f);
	}
}
