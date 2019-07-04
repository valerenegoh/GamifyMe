using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatBar : MonoBehaviour
{
	private Transform cheatBar;
	private float size;
	private bool cheating;
	// Start is called before the first frame update
	void Start()
	{
		cheatBar=transform.Find("bar");
		size=0.01f;
		cheating=false;

		cheatBar.localScale=new Vector3(1f, size);
	}

	public void playerCheating(){
		cheating=true;
	}
	public void playerNotCheating(){
		cheating=false;
	}
	void Update(){
		if(cheating){
			if(size<1f){
				size=size+0.005f;
			}
		}
		else{
			if(size>0.01f){
				size=size-0.001f;
			}
		}
		cheatBar.localScale=new Vector3(1f, size);
	}
}
