using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColissionController : MonoBehaviour
{
    //private GameObject player;
    private GameObject audioObject;
    private AudioSource audioSrc;
    private AudioController audioCtrl;

	// Use this for initialization
	void Start ()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        audioObject = GameObject.FindGameObjectWithTag("AudioManager");
        //audioSrc = audioObject.GetComponent<AudioSource>();
        audioCtrl = audioObject.GetComponent<AudioController>();

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
        else if (collision.gameObject.tag == "Kenny")
        {
            audioCtrl.PlaySplat(); // This plays a splat sound that also calls HeKilledKenny sound 0.2 seconds after
            Destroy(collision.gameObject);
        }
    }
}
