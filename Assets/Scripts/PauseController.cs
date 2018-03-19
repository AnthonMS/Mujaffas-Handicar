using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private bool isPaused = true;
    public bool gameEnded = false;
    [Header("Canvas Stuff")]
    public Canvas endGameCanvas;
    public GameObject endGamePanel;
    public Text deadScoreText;
    public Text deadHighscoreText;

    private GameObject audioMan;
    //private AudioSource effectsSrc; // AudioSource 0
    private AudioSource motorSrc; // AudioSource 1
    //private AudioSource musicSrc; // AudioSource 2
    private CanvasManager canvasMngr;
    private bool muteMotor;

    // Use this for initialization
    void Start ()
    {
        audioMan = GameObject.FindGameObjectWithTag("AudioManager");
        //effectsSrc = audioMan.GetComponents<AudioSource>()[0];
        motorSrc = audioMan.GetComponents<AudioSource>()[1];
        //musicSrc = audioMan.GetComponents<AudioSource>()[2];
        canvasMngr = GameObject.FindGameObjectWithTag("HUDcanvas").GetComponent<CanvasManager>();
        if (canvasMngr.muteTier == 0)
            muteMotor = false;
        else if (canvasMngr.muteTier == 1)
            muteMotor = false;
        else if (canvasMngr.muteTier == 2)
            muteMotor = true;
        motorSrc.mute = true;

        //endGameCanvas.enabled = false;
        endGamePanel.SetActive(false);
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        if (Time.time > 1)
            TabToStart();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space) && gameEnded == false && isPaused)
        {
            TabToStart();
        }

        // Check if the motor is muted or not. But only check when the game is paused. So it is not always called
        if (isPaused)
        {
            if (canvasMngr.muteTier == 0)
                muteMotor = false;
            else if (canvasMngr.muteTier == 1)
                muteMotor = false;
            else if (canvasMngr.muteTier == 2)
                muteMotor = true;
        }
	}

    public void TabToStart()
    {
        isPaused = false;
        if (!isPaused)
        {
            Time.timeScale = 1;
            GameObject.FindGameObjectWithTag("TabToPlay").SetActive(false);
            motorSrc.mute = muteMotor;
        }  
        else
        {
            Time.timeScale = 0;
        }
    }

    public void EndGame()
    {
        //endGameCanvas.enabled = true;
        endGamePanel.SetActive(true);
        gameEnded = true;
        float tempscore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().score;
        deadScoreText.text = "Your score: " + tempscore.ToString("0");
        float temphighscore = PlayerPrefs.GetFloat("Highscore");
        deadHighscoreText.text = "Highscore: " + temphighscore.ToString("0");
        motorSrc.mute = true;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Debug.Log("Restart game!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().firstSpawn = false;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().restartedGame = true;
    }
}
