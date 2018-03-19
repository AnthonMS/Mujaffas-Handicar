using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyleAndStanMotor : MonoBehaviour
{
    private bool walkIn = true;
    private bool walkAway = false;

    private bool tempBool = false;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Follow the background so they are standing still
        float speed = GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>().speed;
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        
        // check if they are further down the x axis than 3, if true, they walk left onto the sidewalk.
        if (walkIn)
        {
            if (transform.position.x > 3)
                transform.Translate(Vector2.left * 2 * Time.deltaTime);
            else
                walkIn = false;
        }
        else
        {
            if (!tempBool)
            {
                Invoke("WalkAway", 2.5f);
                tempBool = true;
            }
        }
        
        if (walkAway)
            transform.Translate(Vector2.right * 2 * Time.deltaTime);
        
    }

    private void WalkAway()
    {
        walkAway = true;
    }
}
