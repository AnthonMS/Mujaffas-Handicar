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

	// Use this for initialization
	void Start ()
    {
        //endGameCanvas.enabled = false;
        endGamePanel.SetActive(false);
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space) && gameEnded == false)
        {
            TabToStart();
        }
	}

    public void TabToStart()
    {
        isPaused = false;
        if (!isPaused)
        {
            Time.timeScale = 1;
            GameObject.FindGameObjectWithTag("TabToPlay").SetActive(false);
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
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Debug.Log("Restart game!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetDeadScoreText()
    {

    }

    private void SetDeadHighscoreText()
    {

    }
}
