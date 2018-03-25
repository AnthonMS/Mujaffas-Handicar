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
    public float tier = 0;
    public float tierIncrease = 100;
    public float maxTier = 8;

    [Header("Canvas Stuff")]
    public Image healthBar;
    public Canvas canvas;

    // Private stuff
    private float maxHealth = 100;
    private float scoreIncreaseSpeed = 10;
    private float lastTier;
    private float lastTierIncrease;

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

    public void AddScore(float score)
    {
        this.score += score;
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

        // Max tier is 10, only increase if lower than max tier
        if (tier < maxTier)
        {
            if (score > lastTierIncrease + tierIncrease)
                IncreaseTier();
        }
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

    private void IncreaseTier()
    {
        //Debug.Log("Tier increased!");
        lastTierIncrease = score;
        tier++;

        scoreIncreaseSpeed += 5;

        GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>().speed += 0.5f;
        GameObject.FindGameObjectWithTag("Road1").GetComponent<BackgroundManager>().speed += 0.5f;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().blueCarSpeed += 0.5f;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().chefsCarSpeed += 0.5f;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().copCarSpeed += 0.5f;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().busSpeed += 0.5f;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().kennyFollowBgSpeed += 0.5f;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().kennyCrossingSpeed += 0.1f;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().spawnEverySec -= 0.3f;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().spawnKennyEverySec -= 0.3f;
        // Increase timmy's speed as well, so it gets a bit easier to dodge the cars
        gameObject.GetComponent<PlayerMotorNew>().speed += 0.3f;
        gameObject.GetComponent<PlayerMotorTouch>().speed += 0.3f;
    }
}
