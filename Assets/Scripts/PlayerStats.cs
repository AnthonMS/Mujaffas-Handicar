using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100;
    public float score = 0;

    [Header("Unity Stuff")]
    public Image healthBar;

    // Private stuff
    private float maxHealth = 100;
    private float minScore = 0;

    // Use this for initialization
    void Start ()
    {
        // hello world
        healthBar.fillAmount = CalculateHealth();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        if (this.health <= 0)
            this.health = 0;
        healthBar.fillAmount = CalculateHealth();
        Debug.Log("You took " + damage + " damage, you have " + health + " left");
    }

    public void AddScore(float score)
    {
        this.score += score;
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
}
