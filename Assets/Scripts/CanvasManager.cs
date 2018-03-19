using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public int muteTier = 0;
    private GameObject audioMan;
    private AudioSource effectsSrc; // AudioSource 0
    private AudioSource motorSrc; // AudioSource 1
    private AudioSource musicSrc; // AudioSource 2
    //private GameObject player;

    [Header("Button Stuff")]
    public Button muteBtn;
    public Sprite speakerFullIcon;
    public Sprite speakerLowIcon;
    public Sprite speakerMuteIcon;

    [Header("Score Stuff")]
    public Text scoreText;

    // Use this for initialization
    void Start ()
    {
        //Debug.Log("Initialize button manager");
        audioMan = GameObject.FindGameObjectWithTag("AudioManager");
        effectsSrc = audioMan.GetComponents<AudioSource>()[0];
        motorSrc = audioMan.GetComponents<AudioSource>()[1];
        musicSrc = audioMan.GetComponents<AudioSource>()[2];
        //player = GameObject.FindGameObjectWithTag("Player");

        InitMuteSettings();
	}

    // Update is called once per frame
	void Update ()
    {

    }

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }

    public void MuteOrUnmute()
    {
        //Debug.Log("Mute clicked!! " + muteTier);
        if (muteTier == 0)
        {
            musicSrc.mute = true;
            //musicSrc.Stop();
            muteBtn.GetComponent<Image>().sprite = speakerLowIcon;
            muteTier = 1;
            // Save the players settings for mute
            PlayerPrefs.SetInt("MuteTier", 0);

        }
        else if (muteTier == 1)
        {
            musicSrc.mute = true;
            motorSrc.mute = true;
            effectsSrc.mute = true;
            muteBtn.GetComponent<Image>().sprite = speakerMuteIcon;
            muteTier = 2;
            // Save the players settings for mute
            PlayerPrefs.SetInt("MuteTier", 1);
        }
        else if (muteTier == 2)
        {
            musicSrc.mute = false;
            //musicSrc.Play();
            motorSrc.mute = false;
            effectsSrc.mute = false;
            muteBtn.GetComponent<Image>().sprite = speakerFullIcon;
            muteTier = 0;
            // Save the players settings for mute
            PlayerPrefs.SetInt("MuteTier", 2);
        }
    }

    private void InitMuteSettings()
    {
        if (PlayerPrefs.HasKey("MuteTier"))
        {
            //Debug.Log("Playerprefs HAS key MuteTier");
            muteTier = PlayerPrefs.GetInt("MuteTier");
            MuteOrUnmute();

        }
        else
        {
            //Debug.Log("Playerprefs HAS NO key MuteTier");
        }
    }
}
