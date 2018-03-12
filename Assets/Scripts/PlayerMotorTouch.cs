using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotorTouch : MonoBehaviour
{

    // Used for touch input
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    // Used for moving the player
    public float speed = 5f;
    private const float LANE_DISTANCE = 2f;
    private const float FRONT_BACK_DISTANCE = 3f;

    private GameObject player;
    private GameObject laneTarget;
    // All the positions for the LaneTarget
    private Vector2 leftLaneBack, middleLaneBack, rightLaneBack, leftLaneFront, middleLaneFront, rightLaneFront;
    private int lane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private int backFront = 0; // 0 = Back, 1 = Front

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        laneTarget = GameObject.FindGameObjectWithTag("TargetPoint");
        InitializeLanePos();
    }
	
	// Update is called once per frame
	void Update ()
    {
        SwipeDetection();
        player.transform.position = Vector2.MoveTowards(player.transform.position, laneTarget.transform.position, speed * Time.deltaTime);
    }

    private void SwipeDetection()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            //Debug.Log("Right Swipe");
                            MoveLane(true);
                            //Debug.Log("Lane: " + lane + ", FrontBack: " + backFront);
                            MoveLaneTarget(lane, backFront);
                        }
                        else
                        {   //Left swipe
                            //Debug.Log("Left Swipe");
                            MoveLane(false);
                            //Debug.Log("Lane: " + lane + ", FrontBack: " + backFront);
                            MoveLaneTarget(lane, backFront);
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            //Debug.Log("Up Swipe");
                            MoveFrontBack(true);
                            //Debug.Log("Lane: " + lane + ", FrontBack: " + backFront);
                            MoveLaneTarget(lane, backFront);
                        }
                        else
                        {   //Down swipe
                            //Debug.Log("Down Swipe");
                            MoveFrontBack(false);
                            //Debug.Log("Lane: " + lane + ", FrontBack: " + backFront);
                            MoveLaneTarget(lane, backFront);
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }

    private void MoveLane(bool goingRight)
    {
        // This adds 1 if goingRight, else it retracts 1
        lane += (goingRight) ? 1 : -1;
        // This clamps lane, so lowest number is 0, and highest is 2
        lane = Mathf.Clamp(lane, 0, 2);
    }

    private void MoveFrontBack(bool goingForward)
    {
        backFront += (goingForward) ? 1 : -1;
        backFront = Mathf.Clamp(backFront, 0, 1);
    }

    private void MoveLaneTarget(int tempLane, int tempBackFront)
    {
        if (tempBackFront == 0)
        {
            if (tempLane == 0)
            {
                laneTarget.transform.position = leftLaneBack;
            }
            else if (tempLane == 1)
            {
                laneTarget.transform.position = middleLaneBack;
            }
            else if (tempLane == 2)
            {
                laneTarget.transform.position = rightLaneBack;
            }
        }
        else if (tempBackFront == 1)
        {
            if (tempLane == 0)
            {
                laneTarget.transform.position = leftLaneFront;
            }
            else if (tempLane == 1)
            {
                laneTarget.transform.position = middleLaneFront;
            }
            else if (tempLane == 2)
            {
                laneTarget.transform.position = rightLaneFront;
            }
        }
    }

    private void InitializeLanePos()
    {
        // Middle lanes
        Vector2 tempPos = laneTarget.transform.position;
        middleLaneBack = tempPos;
        tempPos.y = tempPos.y + FRONT_BACK_DISTANCE;
        middleLaneFront = tempPos;
        // Left lanes
        tempPos = laneTarget.transform.position;
        tempPos.x = tempPos.x - LANE_DISTANCE;
        leftLaneBack = tempPos;
        tempPos.y = tempPos.y + FRONT_BACK_DISTANCE;
        leftLaneFront = tempPos;
        // Right lanes
        tempPos = laneTarget.transform.position;
        tempPos.x = tempPos.x + LANE_DISTANCE;
        rightLaneBack = tempPos;
        tempPos.y = tempPos.y + FRONT_BACK_DISTANCE;
        rightLaneFront = tempPos;
    }
}
