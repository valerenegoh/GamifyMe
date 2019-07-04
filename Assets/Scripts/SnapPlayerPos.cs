using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPlayerPos : MonoBehaviour
{
	public float x;
	public float y;
	private GameObject Player;
	private PlayerController PlayerController;

    // Start is called before the first frame update
    void Start()
    {
    	Player = GameObject.Find("Player");
        PlayerController=Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.tag=="Player"){
			StartCoroutine(Freeze(1f));
			other.transform.eulerAngles = new Vector3(0,0,0);
			other.transform.position = new Vector3(x, y, 0);
		}
	}

	IEnumerator Freeze(float time){
		PlayerController.movable = false;
		yield return new WaitForSeconds(time);
		PlayerController.movable = true;
	}
}
