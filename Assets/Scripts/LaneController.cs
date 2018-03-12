using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    private GameObject player;
    private PlayerMotorNew playerMotor;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMotor = player.GetComponent<PlayerMotorNew>();
	}

	// Update is called once per frame
	void Update ()
    {
        transform.Translate(new Vector2(0, playerMotor.speed * Time.deltaTime));
    }
}
