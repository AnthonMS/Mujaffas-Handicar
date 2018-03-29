using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSawner : MonoBehaviour
{
    [Header("Pickup Items")]
    private string booster = "Booster";
    private string repair = "Repair";
    private string shield = "Shield_Pickup";
    private string lastPickup;

    [Header("Variables")]
    public float spawnEverySec;
    private float lastSpawn;

    private Vector2 leftLane = new Vector2(-1.75f, 5);
    private Vector2 rightLane = new Vector2(1.75f, 5);
    private Vector2 middleLane = new Vector2(0, 5);

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > lastSpawn + spawnEverySec)
        {
            SpawnPickup();
            lastSpawn = Time.time;
        }
	}

    private void SpawnPickup()
    {
        string tempPickup = GetRandomPickup();
        Vector2 tempPos = GetRandomLane();
        GameObject pickupInstance = Instantiate(Resources.Load(tempPickup, typeof(GameObject))) as GameObject;
        pickupInstance.transform.Translate(tempPos);
        pickupInstance.transform.parent = transform;
    }

    private Vector2 GetRandomLane()
    {
        Vector2 tempVec;
        int randomInt = Random.Range(0, 101); // Random Int from 0-100
        if (randomInt < 33)
        {
            //Debug.Log("Below 33");
            tempVec = rightLane;
        }
        else if (randomInt > 66)
        {
            //Debug.Log("Above 66");
            tempVec = middleLane;
        }
        else
        {
            //Debug.Log("Between 33 and 66");
            tempVec = leftLane;
        }
        return tempVec;
    }

    string tempObj;
    private string GetRandomPickup()
    {
        tempObj = null;
        bool tempBool = true;
        while (tempBool)
        {
            int randomInt = Random.Range(0, 101); // Random Int from 0-100
            if (randomInt < 33)
            {
                //Debug.Log("Below 33");
                tempObj = booster;
            }
            else if (randomInt > 66)
            {
                //Debug.Log("Above 66");
                tempObj = repair;
            }
            else
            {
                //Debug.Log("Between 33 and 66");
                tempObj = shield;
            }

            if (tempObj != lastPickup)
            {
                tempBool = false;
                lastPickup = tempObj;
            }
        }
        
        return tempObj;
    }
}
