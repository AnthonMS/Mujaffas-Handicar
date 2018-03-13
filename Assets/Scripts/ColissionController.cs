using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColissionController : MonoBehaviour
{
    //private GameObject player;

	// Use this for initialization
	void Start ()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Car")
        {
            Debug.Log("You got hit by a car!");
        }
    }
}
