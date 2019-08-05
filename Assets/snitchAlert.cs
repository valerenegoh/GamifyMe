using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snitchAlert : MonoBehaviour
{
    private Transform exclaimationMark;
    private Transform exclaimationMark_GameObject;
    private SpriteRenderer exclaimationMark_renderer;
    private float size = 0f;
    private bool snitchRange = false;

    // Start is called before the first frame update
    void Start()
    {
        exclaimationMark_GameObject = transform.Find("ExclaimationMark");
        exclaimationMark = transform.Find("ExclaimationMark").Find("SpriteRendererOuter").Find("SpriteFillColor");
        exclaimationMark_renderer = exclaimationMark.GetComponentInChildren<SpriteRenderer>();
        size = 0f;
        exclaimationMark.localScale = new Vector3(1f, 0f);
        exclaimationMark_GameObject.gameObject.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        if (size < 1f && snitchRange)
        {
            size = size + 0.01f;
        }
        else if (size > 0f && !snitchRange)
        {
            size = size - 0.01f;
        }
        if (size <= 0f)
        {
            exclaimationMark_GameObject.gameObject.SetActive(false);
        }
        if (size < 1f)
        {
            exclaimationMark.localScale = new Vector3(1f, size);
        }
        else
        {
            exclaimationMark.localScale = new Vector3(1f, 1f);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            snitchRange = true;
            exclaimationMark_GameObject.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            snitchRange = false;
        }
    }
}
