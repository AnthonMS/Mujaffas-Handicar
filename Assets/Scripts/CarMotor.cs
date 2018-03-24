using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotor : MonoBehaviour
{
    public float speed = 3.5f;
    public float retractScore = 5f;
    public float damage = 10f;
    //private GameObject car;
    private float verticalHeight;
    //private GameObject spawner;

    // Use this for initialization
    void Start ()
    {
        //car = this.gameObject;
        verticalHeight = GetComponent<Renderer>().bounds.size.y;
        //spawner = GameObject.FindGameObjectWithTag("ObstacleSpawner");
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(new Vector2(0, -speed * Time.deltaTime));
        CheckIfBelowScreen();
	}

    private void CheckIfBelowScreen()
    {
        float dist = (transform.position - Camera.main.transform.position).z;
        float Bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        if (Bottom > transform.position.y + verticalHeight)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == "Car")
        {
            
            if (collision.transform.position.y > transform.position.y)
            {
                // If the car that hits is behind this car
                //Debug.Log(collision.name+" hit other car");
                Vector2 tempVec = collision.transform.position;
                tempVec.y += 1.5f;
                collision.transform.position = tempVec;
            }
            else
            {
                //Debug.Log(collision.name + " got hit from behind");
            }
        }
    }
}
