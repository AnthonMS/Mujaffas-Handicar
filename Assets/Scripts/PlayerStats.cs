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
    private bool justHit = false;
    private bool isProtecting = false;
    private bool isBoosting = false;
    private float boostingTime = 7f;

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
    // Start boosting, by making bg's, cars's, Jimmy's, and Kenny's speed * 2 
    public void SetBoosting(bool tempBool)
    {
        if (!isBoosting)
        {
            Invoke("StopBoosting", boostingTime);

            scoreIncreaseSpeed *= 2;
            isProtecting = tempBool;
            GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().isBoosting = tempBool;
            //GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().spawnEverySec /= 2;
            isBoosting = tempBool;
            GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>().speed *= 2;
            GameObject.FindGameObjectWithTag("Road1").GetComponent<BackgroundManager>().speed *= 2;

            GameObject[] carList = GameObject.FindGameObjectsWithTag("Car");
            if (carList.Length > 0)
            {
                foreach (GameObject car in carList)
                {
                    car.GetComponent<CarMotor>().speed *= 2;
                }
            }
            GameObject tempJim = GameObject.FindGameObjectWithTag("Jimmy");
            if (tempJim != null)
                tempJim.GetComponent<JimmyMotor>().speed *= 2;
        }
    }
    // Stop boosting, by making bg's, cars's, Jimmy's, and Kenny's speed / 2 
    private void StopBoosting()
    {
        scoreIncreaseSpeed /= 2;
        isProtecting = false;
        GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().isBoosting = false;
        //GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().spawnEverySec *= 2;
        isBoosting = false;
        //BackgroundManager tempRoad = GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>();
        GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>().speed /= 2;
        GameObject.FindGameObjectWithTag("Road1").GetComponent<BackgroundManager>().speed /= 2;
        GameObject[] carList = GameObject.FindGameObjectsWithTag("Car");
        if (carList.Length > 0)
        {
            //Debug.Log("There are " + carList.Length + " Cars in the screen");
            foreach (GameObject car in carList)
            {
                car.GetComponent<CarMotor>().speed /= 2;
            }
        }
        GameObject tempJim = GameObject.FindGameObjectWithTag("Jimmy");
        if (tempJim != null)
            tempJim.GetComponent<JimmyMotor>().speed /= 2;
    }

    public void TakeDamage(float damage)
    {
        if (!isProtecting && !justHit)
        {
            justHit = true;
            Invoke("SetJustHit", 0.5f);
            this.health -= damage;
            if (this.health <= 0)
            {
                this.health = 0;


                // Call this method with Invoke, because if we call it right away, it shows the wrong score in the EndGame menu
                Invoke("SendEndGameMessage", 0.05f);
            }
            healthBar.fillAmount = CalculateHealth();
        }
        
    }

    private void SetJustHit()
    {
        justHit = false;
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
