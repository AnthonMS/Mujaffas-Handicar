using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSrc;
    [Header("Audio Clips")]
    public AudioClip heKilledKenny;
    public AudioClip splat;
    public AudioClip crash;

    [Header("Timmy Sounds")]
    public AudioClip greetJimmy;
    public AudioClip ahh_Timmy_retarded;
    public AudioClip hua, hua_2, hua_3, hua_4, hua_5;
    public AudioClip luAla_Timmy, luAla_Timmy_2;
    public AudioClip tim_Timmy, tim_Timmy_2, tim_Timmy_3;
    public AudioClip timmy_Scream;
    public AudioClip timmy_Shit;
    public AudioClip timmy_timmy;
    public AudioClip timmy, timmy_2, timmy_3, timmy_4, timmy_5, timmy_6, timmy_7, timmy_8, timmy_9, timmy_10, timmy_11, timmy_12;
    public AudioClip uah_Timmy;

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

    public void PlayCrash()
    {
        audioSrc.PlayOneShot(crash);
    }

    public void GreetJimmySound()
    {
        // Check if the audio source is playing, if not, play greeting.
        // This Includes the crash sound and HeKilledKenny sound
        if (!audioSrc.isPlaying)
            audioSrc.PlayOneShot(greetJimmy);
    }

    public void PlayTimmySound()
    {

    }

    private AudioClip GetTimmySound()
    {
        AudioClip tempClip = ahh_Timmy_retarded;

        return tempClip;
    }
}
