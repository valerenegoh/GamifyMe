using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatingPopup : MonoBehaviour
{
	private const float DISAPPEAR_TIMER_MAX=1f;
	private TextMeshPro textMesh;
	private float disappearTimer;
	private Color textColor;
	private static int sortingOrder;

	private void Awake(){
		textMesh=transform.GetComponent<TextMeshPro>();
	}
	public void Setup(string cheatingText,bool isCriticalHit){
		textMesh.SetText(cheatingText);
		if(!isCriticalHit){
			textMesh.fontSize=20;
			//Color32 RGB 0-255 encoding
			textMesh.color= new Color32(0, 255,24, 255);
		}else{
			textMesh.fontSize=20;
			textMesh.color= new Color32(134, 255,134, 255);
		}
		textColor=textMesh.color;
		disappearTimer=DISAPPEAR_TIMER_MAX;

		sortingOrder++;
		textMesh.sortingOrder=sortingOrder;
	}
	private void Update(){
		float moveYSpeed=0.4f;
		float moveXSpeed=0.2f;
		transform.position+=new Vector3(moveXSpeed,moveYSpeed)*Time.deltaTime;

		if(disappearTimer >DISAPPEAR_TIMER_MAX*.5f){
			//first half of dissapearing
			float increaseScaleAmount=0.05f;
			transform.localScale +=Vector3.one *increaseScaleAmount*Time.deltaTime;
		}
		else{
			//second half of dissapearing
			float decreaseScaleAmount=0.05f;
			transform.localScale -=Vector3.one *decreaseScaleAmount*Time.deltaTime;
		}

		disappearTimer -= Time.deltaTime;


		if(disappearTimer<0){
			//Start disappearing
			float disappearSpeed=0.5f;
			textColor.a -= disappearSpeed*Time.deltaTime;
			textMesh.color=textColor;
			if(textColor.a<0){
				Destroy(gameObject);
			}
		}
	}
}
