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
    public AudioClip seeYouTimTim;
    public AudioClip crash_explosion;
    public AudioClip crash_shielded;
    public AudioClip rocket_thrust;
    public AudioClip shield_pickup;
    public AudioClip repair_pickup;

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

    public void PlayExplosion()
    {
        audioSrc.PlayOneShot(crash_explosion);
    }

    public void PlayCrashShielded()
    {
        audioSrc.PlayOneShot(crash_shielded);
    }

    public void GreetJimmySound()
    {
        // Check if the audio source is playing, if not, play greeting.
        // This Includes the crash sound and HeKilledKenny sound
        //if (!audioSrc.isPlaying)
            audioSrc.PlayOneShot(greetJimmy);
        Invoke("GreetTimmySound", 0.7f);
    }

    private void GreetTimmySound()
    {
        audioSrc.PlayOneShot(seeYouTimTim);
    }

    public void PlayTimmySound()
    {
        if (!audioSrc.isPlaying)
            audioSrc.PlayOneShot(GetTimmySound());
    }

    public void PlayRocketThrust()
    {
        audioSrc.PlayOneShot(rocket_thrust);
    }

    public void PlayShieldPickup()
    {
        audioSrc.PlayOneShot(shield_pickup);
    }

    public void PlayRepairPickup()
    {
        audioSrc.PlayOneShot(repair_pickup);
    }

    private AudioClip GetTimmySound()
    {
        AudioClip tempClip = ahh_Timmy_retarded;
        int randomInt = Random.Range(1, 28);

        switch(randomInt)
        {
            case 1: tempClip = ahh_Timmy_retarded; break;
            case 2: tempClip = hua; break;
            case 3: tempClip = hua_2; break;
            case 4: tempClip = hua_3; break;
            case 5: tempClip = hua_4; break;
            case 6: tempClip = hua_5; break;
            case 7: tempClip = luAla_Timmy; break;
            case 8: tempClip = luAla_Timmy_2; break;
            case 9: tempClip = tim_Timmy; break;
            case 10: tempClip = tim_Timmy_2; break;
            case 11: tempClip = tim_Timmy_3; break;
            case 12: tempClip = timmy_Scream; break;
            case 13: tempClip = timmy_Shit; break;
            case 14: tempClip = timmy_timmy; break;
            case 15: tempClip = timmy; break;
            case 16: tempClip = timmy_2; break;
            case 17: tempClip = timmy_3; break;
            case 18: tempClip = timmy_4; break;
            case 19: tempClip = timmy_5; break;
            case 20: tempClip = timmy_6; break;
            case 21: tempClip = timmy_7; break;
            case 22: tempClip = timmy_8; break;
            case 23: tempClip = timmy_9; break;
            case 24: tempClip = timmy_10; break;
            case 25: tempClip = timmy_11; break;
            case 26: tempClip = timmy_12; break;
            case 27: tempClip = uah_Timmy; break;
        }

        return tempClip;
    }
}
