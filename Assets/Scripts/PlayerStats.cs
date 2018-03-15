using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private float maxHealth = 100;
    public float health = 100;
    public float score = 0;
    private float minScore = 0;
    public Slider healthBar;

	// Use this for initialization
	void Start ()
    {
        healthBar.value = CalculateHealth();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        healthBar.value = CalculateHealth();
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
