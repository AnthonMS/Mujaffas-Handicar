using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip heKilledKenny;
    public AudioClip splat;
    private AudioSource audioSrc;

    // Use this for initialization
    void Start ()
    {
        audioSrc = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayHeKilledKenny()
    {
        //Debug.Log("Play he killed kenny sounds!!");
        audioSrc.PlayOneShot(heKilledKenny);
    }

    public void PlaySplat()
    {
        audioSrc.PlayOneShot(splat);
        Invoke("PlayHeKilledKenny", 0.2f);
    }
}
