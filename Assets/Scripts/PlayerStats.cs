using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100;
    public float score = 0;
    public float highScore;

    [Header("Canvas Stuff")]
    public Image healthBar;
    public Canvas canvas;

    // Private stuff
    private float maxHealth = 100;
    private float scoreIncreaseSpeed = 10;

    // Use this for initialization
    void Start ()
    {
        // hello world
        healthBar.fillAmount = CalculateHealth();

        CheckHighscore();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CalculateScore();
        
	}

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.health = 0;
            

            // Call this method with Invoke, because if we call it right away, it shows the wrong score in the EndGame menu
            Invoke("SendEndGameMessage", 0.05f);
        }
        healthBar.fillAmount = CalculateHealth();
        //Debug.Log("You took " + damage + " damage, you have " + health + " left");
    }

    private void SendEndGameMessage()
    {
        SetHighscore(); // Set the highscore here, else the wrong highscore is displayed if it was beaten.
        GameObject.FindGameObjectWithTag("PauseController").SendMessage("EndGame");
    }

    public void RetractScore(float score)
    {
        this.score -= score;
        if (this.score <= 0)
            this.score = 0;
    }

    private float CalculateHealth()
    {
        return this.health / this.maxHealth;
    }

    private void CalculateScore()
    {
        float tempScore = score + scoreIncreaseSpeed * Time.deltaTime;
        score = tempScore;
        canvas.SendMessage("SetScoreText", score.ToString("0"));
    }

    public void SetHighscore()
    {
        if (score > highScore)
        {
            highScore = score;
            //Debug.Log("Highscore beat! New = " + highScore);
            PlayerPrefs.SetFloat("Highscore", highScore);
        }
    }

    private void CheckHighscore()
    {
        if (PlayerPrefs.HasKey("Highscore"))
            highScore = PlayerPrefs.GetFloat("Highscore");
    }
}
