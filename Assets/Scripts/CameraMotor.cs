using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

    private GameObject player;
    private Vector3 playerPos;
    private Vector3 offset;
    public float speed = 5f;

	// Use this for initialization
	void Start ()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //offset = transform.position - player.transform.position;
        //playerPos = player.transform.position;

    }

    // Update is called once per frame
    void LateUpdate ()
    {
        //transform.position = new Vector3(playerPos.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
        
    }
}
