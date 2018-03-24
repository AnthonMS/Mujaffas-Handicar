using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyMotor : MonoBehaviour
{
    private float speed;
    public bool hasGreeted = false;
    public float addScore = 50;

	// Use this for initialization
	void Start ()
    {
        speed = GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>().speed;
        speed += 0.5f;
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

        if (Bottom > transform.position.y + GetComponent<Renderer>().bounds.size.y)
        {
            Destroy(gameObject);
        }
    }
}
