using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private int muteTier = 0;
    private GameObject audioMan;
    private AudioSource effectsSrc; // AudioSource 0
    private AudioSource motorSrc; // AudioSource 1
    private AudioSource musicSrc; // AudioSource 2

    [Header("Button Stuff")]
    public Button muteBtn;
    public Sprite speakerFullIcon;
    public Sprite speakerLowIcon;
    public Sprite speakerMuteIcon;

    // Use this for initialization
    void Start ()
    {
        //Debug.Log("Initialize button manager");
        audioMan = GameObject.FindGameObjectWithTag("AudioManager");
        effectsSrc = audioMan.GetComponents<AudioSource>()[0];
        motorSrc = audioMan.GetComponents<AudioSource>()[1];
        musicSrc = audioMan.GetComponents<AudioSource>()[2];
	}

    // Update is called once per frame
	void Update ()
    {

    }

    public void MuteOrUnmute()
    {
        //Debug.Log("Mute or Unmute audio");
        if (muteTier == 0)
        {
            Debug.Log("Mute Music!");
            musicSrc.mute = true;
            //musicSrc.Stop();
            muteBtn.GetComponent<Image>().sprite = speakerLowIcon;
            muteTier = 1;
        }
        else if (muteTier == 1)
        {
            Debug.Log("Mute Music AND effects");
            musicSrc.mute = true;
            motorSrc.mute = true;
            effectsSrc.mute = true;
            muteBtn.GetComponent<Image>().sprite = speakerMuteIcon;
            muteTier = 2;
        }
        else if (muteTier == 2)
        {
            Debug.Log("Unmute Music AND effects");
            musicSrc.mute = false;
            //musicSrc.Play();
            motorSrc.mute = false;
            effectsSrc.mute = false;
            muteBtn.GetComponent<Image>().sprite = speakerFullIcon;
            muteTier = 0;
        }
    }
}
