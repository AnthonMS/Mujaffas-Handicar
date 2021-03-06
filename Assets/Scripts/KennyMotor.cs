﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KennyMotor : MonoBehaviour
{
    public float damage = 10f;
    public float retractScore = 10f;
    public float followBgSpeed = 2f;
    public float crossingSpeed = 1f;
    private bool goLeft = true;

	// Use this for initialization
	void Start ()
    {
        // This will just destroy Kenny if he lives longer than 15 seconds. (He will be long gone fromt he screen by then)
        Destroy(gameObject, 15f);
        followBgSpeed = GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>().speed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        followBgSpeed = GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>().speed;
        if (goLeft)
        {
            transform.Translate(new Vector2(-crossingSpeed * Time.deltaTime, -followBgSpeed * Time.deltaTime));
        }
        else
        {
            transform.Translate(new Vector2(crossingSpeed * Time.deltaTime, -followBgSpeed * Time.deltaTime));
        }

    }

    public void ChangeDir(bool goLeft)
    {
        this.goLeft = goLeft;
        //Debug.Log("Change dir was called!");
        if (!goLeft)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Car")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            //Destroy(gameObject);
        }
    }
}
