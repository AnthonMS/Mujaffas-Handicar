using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float lastTime;
    public float spawnEverySec = 4;
    private float lastKenny;
    public float spawnKennyEverySec = 14;

    //private float leftLane = -1.75f;
    //private float rightLane = 1.75f;
    //private float middleLane = 0f;
    private Vector2 leftLane = new Vector2(-1.75f, 5);
    private Vector2 rightLane = new Vector2(1.75f, 5);
    private Vector2 middleLane = new Vector2(0, 5);
    

	// Use this for initialization
	void Start ()
    {
        Obstacle1();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Time.time > lastTime + spawnEverySec)
        {
            //Debug.Log(Time.time);
            lastTime = Time.time;
            WhichObstacle();
        }
        if (Time.time > lastKenny + spawnKennyEverySec)
        {
            SpawnKenny();
            lastKenny = Time.time;
        }
    }

    private void WhichObstacle()
    { // 1, 2, 4, 5, 6
        int randomInt = Random.Range(0, 101);
        if (randomInt <= 10) // If smaller than 10 - 10%
            Obstacle4();
        else if (randomInt <= 20 && randomInt > 10) // if smaller than 20 and bigger than 10 - 10%
            Obstacle6();
        else if (randomInt <= 40 && randomInt > 20) // if smaller than 35 and bigger than 20 - 20%
            Obstacle5();
        else if (randomInt <= 60 && randomInt > 40) // if smaller than 50 and bigger than 35 - 20%
            Obstacle2();
        else if (randomInt <= 70 && randomInt > 60) // if smaller than 60 and bigger than 50 - 10%
            Obstacle7();
        else if (randomInt <= 80 && randomInt > 70) // if smaller than 70 and bigger than 60 - 10%
            Obstacle6();
        else if (randomInt <= 100 && randomInt > 80) // if smaller than 100 and bigger than 70 - 20%
            Obstacle1();
        else
            Debug.Log("No Obstacle was spawned! " + randomInt);
        
    }
    // Random lane, Blue car
    private void Obstacle1()
    {
        Vector2 templane = GetRandomLane();
        GameObject carInstance = Instantiate(Resources.Load("Blue_car", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(templane);
        carInstance.transform.parent = transform;
        //ExtraObstacle();
        if (ExtraObstacle() == false)
        {
            //Debug.Log("Extra Obstacle returned as false");
            ExtraDoubleObstacle();
        }
    }
    // Random lane, Chefs car
    private void Obstacle2()
    {
        GameObject carInstance = Instantiate(Resources.Load("Chefs_car", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(GetRandomLane());
        carInstance.transform.parent = transform;
        //ExtraObstacle();
        if (ExtraObstacle() == false)
        {
            //Debug.Log("Extra Obstacle returned as false");
            ExtraDoubleObstacle();
        }
    }
    // Left & Right lane, Chefs car & Blue car
    private void Obstacle4()
    {
        GameObject carInstance = Instantiate(Resources.Load("Chefs_car", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(leftLane);
        carInstance.transform.parent = transform;
        carInstance = Instantiate(Resources.Load("Blue_car", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(rightLane);
        carInstance.transform.parent = transform;
    }
    // Random lane, School bus
    private void Obstacle5()
    {
        GameObject carInstance = Instantiate(Resources.Load("School_bus", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(GetRandomLane());
        carInstance.transform.parent = transform;
        //ExtraObstacle();
        if (ExtraObstacle() == false)
        {
            //Debug.Log("Extra Obstacle returned as false");
            ExtraDoubleObstacle();
        }
    }
    // Random lane, Cop car
    private void Obstacle6()
    {
        Vector2 tempLane = GetRandomLane();
        tempLane.y = tempLane.y + 2f;
        GameObject carInstance = Instantiate(Resources.Load("Cop_car", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(tempLane);
        carInstance.transform.parent = transform;
        //ExtraObstacle();
        if (ExtraObstacle() == false)
        {
            //Debug.Log("Extra Obstacle returned as false");
            ExtraDoubleObstacle();
        }
    }
    //Left lane & Middle lane, Cop car & Bus
    private void Obstacle7()
    {
        GameObject carInstance = Instantiate(Resources.Load("Cop_car", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(leftLane);
        carInstance.transform.parent = transform;
        carInstance = Instantiate(Resources.Load("School_bus", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(middleLane);
        carInstance.transform.parent = transform;
    }
    // Random lane, right behind other car, blue car
    private bool ExtraObstacle()
    {
        int randomInt = Random.Range(0, 101); // Random Int from 0-100
        if (randomInt < 60)
        {
            Vector2 tempLane = GetRandomLane();
            tempLane.y = tempLane.y + 3.5f;
            GameObject carInstance = Instantiate(Resources.Load("Blue_car", typeof(GameObject))) as GameObject;
            carInstance.transform.Translate(tempLane);
            carInstance.transform.parent = transform;
            return true;
        }
        else
        {
            return false;
        }
    }
    // Left & Right lane, right behind car in middle lane.
    private void ExtraDoubleObstacle()
    {
        Vector2 tempLaneLeft = leftLane;
        tempLaneLeft.y = tempLaneLeft.y + 6f;
        Vector2 tempLaneRight = rightLane;
        tempLaneRight.y = tempLaneRight.y + 6f;
        GameObject carInstance = Instantiate(Resources.Load("Blue_car", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(tempLaneLeft);
        carInstance.transform.parent = transform;
        carInstance = Instantiate(Resources.Load("Blue_car", typeof(GameObject))) as GameObject;
        carInstance.transform.Translate(tempLaneRight);
        carInstance.transform.parent = transform;
    }

    private void SpawnKenny()
    {
        // Calculate if he spawns left or right
        int randomInt = Random.Range(0, 101);
        float tempFloat;
        bool goLeft;
        if (randomInt < 50) { tempFloat = 2f; goLeft = true; }
        else { tempFloat = -8f; goLeft = false; }
        // Calculate how far up he spawns and set the position
        float randomFloat = Random.Range(-5, 4);
        Vector2 tempPos = rightLane;
        tempPos.x = tempPos.x + tempFloat;
        tempPos.y = tempPos.y + randomFloat;
        // Instantiate kenny
        GameObject kennyInstance = Instantiate(Resources.Load("Kenny", typeof(GameObject))) as GameObject;
        kennyInstance.transform.Translate(tempPos);
        kennyInstance.transform.parent = transform;
        // Call ChangeDir, so if goLeft is false, he will go left to right and Sprite is flipped
        kennyInstance.SendMessage("ChangeDir", goLeft);
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
}
