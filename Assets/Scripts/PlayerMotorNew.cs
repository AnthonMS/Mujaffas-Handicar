using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotorNew : MonoBehaviour
{

    public float speed = 5f;
    public float forwardLength = 5f;

    private int lane = 1; // 0 for left lane, 1 for middle lane, 2 for right lane
    //private int desiredLane = 1;
    private const float LANE_DISTANCE = 2.5f;
    private GameObject leftLane, middleLane, rightLane;
    private Vector2 orgPos;
    private Vector2 frontPos;
    private Vector2 backPos;
    private bool moveUp = false;
    private bool moveDown = false;
    private bool moveSide = false;
    private bool canMoveUp = true;
    private bool canMoveDown = false;
    private bool canMoveSide = true;
    private Vector2 leftOrgPos, middleOrgPos, rightOrgPos, newLeftPos, newMiddlePos, newRightPos;

    // Use this for initialization
    void Start()
    {
        leftLane = GameObject.FindGameObjectWithTag("LeftLane");
        middleLane = GameObject.FindGameObjectWithTag("MiddleLane");
        rightLane = GameObject.FindGameObjectWithTag("RightLane");
        orgPos = transform.position;
        frontPos = new Vector2(orgPos.x, orgPos.y + 2.5f);
        backPos = orgPos;
        leftOrgPos = leftLane.transform.position;
        middleOrgPos = middleLane.transform.position;
        rightOrgPos = rightLane.transform.position;
        newLeftPos = new Vector2(leftLane.transform.position.x, leftLane.transform.position.y + 2.5f);
        newMiddlePos = new Vector2(middleLane.transform.position.x, middleLane.transform.position.y + 2.5f);
        newRightPos = new Vector2(rightLane.transform.position.x, rightLane.transform.position.y + 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // Gather keyboard Input
        if (Input.GetKeyDown(KeyCode.LeftArrow) && canMoveSide)
        {
            // Move left
            MoveLane(false);
            canMoveUp = false;
            canMoveDown = false;
            //Debug.Log("Lane: " + lane);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && canMoveSide)
        {
            // Move right
            MoveLane(true);
            canMoveUp = false;
            canMoveDown = false;
            //Debug.Log("Lane: " + lane);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canMoveUp)
        {
            moveDown = false;
            moveUp = true;
            canMoveDown = true;
        }
            

        if (Input.GetKeyDown(KeyCode.DownArrow) && canMoveDown)
        {
            moveUp = false;
            moveDown = true;
            canMoveUp = true;
        }
            

        if (moveDown)
            MoveDown();

        if (moveUp)
            MoveUp();

        // Move the player to the lane
        if (moveSide)
            MovePlayer();
    }

    private void MovePlayer()
    {
        if (lane == 1)
        {
            //Debug.Log("MOVE TO THE MIDDLE");
            transform.position = Vector2.MoveTowards(transform.position, middleLane.transform.position, speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, middleLane.transform.position) <= 0)
            {
                frontPos = new Vector2(transform.position.x, orgPos.y + 2.5f);
                backPos = middleOrgPos;
                moveSide = false;
                //Debug.Log("You are at position");
                canMoveUp = true;
                canMoveDown = true;
            }
                
        }
        else if (lane == 0)
        {
            //Debug.Log("MOVE TO THE LEFT LANE");
            transform.position = Vector2.MoveTowards(transform.position, leftLane.transform.position, speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, leftLane.transform.position) <= 0)
            {
                frontPos = new Vector2(transform.position.x, orgPos.y + 2.5f);
                backPos = leftOrgPos;
                moveSide = false;
                //Debug.Log("You are at position");
                canMoveUp = true;
                canMoveDown = true;
            }
        }
        else if (lane == 2)
        {
            //Debug.Log("MOVE TO THE RIGHT LANE");
            transform.position = Vector2.MoveTowards(transform.position, rightLane.transform.position, speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, rightLane.transform.position) <= 0)
            {
                frontPos = new Vector2(transform.position.x, orgPos.y + 2.5f);
                backPos = rightOrgPos;
                moveSide = false;
                //Debug.Log("You are at position");
                canMoveUp = true;
                canMoveDown = true;
            }
        }
    }

    private void MoveLane(bool goingRight)
    {
        // This adds 1 if goingRight, else it retracts 1
        lane += (goingRight) ? 1 : -1;
        // This clamps lane, so lowest number is 0, and highest is 2
        lane = Mathf.Clamp(lane, 0, 2);
        moveSide = true;
    }

    private void MoveUp()
    {
        // Make it so you can't move to the side when moving forward
        canMoveSide = false;
        transform.position = Vector2.MoveTowards(transform.position, frontPos, speed * Time.deltaTime);

        // Move the lane points to the forward position as well
        leftLane.transform.position = newLeftPos;
        middleLane.transform.position = newMiddlePos;
        rightLane.transform.position = newRightPos;

        // Check if player has reached forward position
        if (Vector3.Distance(transform.position, frontPos) <= 0)
        {
            canMoveSide = true; // Make player able to move to the sides again
            canMoveUp = false; // Make player unable to move more forward
            canMoveDown = true; // Make player able to move backwards
            moveUp = false; // The player is no longer moving forward
        }
    }

    private void MoveDown()
    {
        // Make it so you can't move to the side when moving backward
        canMoveSide = false;
        transform.position = Vector2.MoveTowards(transform.position, backPos, speed * Time.deltaTime);

        // Move the lane points to the original position as well
        leftLane.transform.position = leftOrgPos;
        middleLane.transform.position = middleOrgPos;
        rightLane.transform.position = rightOrgPos;

        // Check if player has reached original position again
        if (Vector3.Distance(transform.position, backPos) <= 0)
        {
            canMoveSide = true; // Make player able to move to the sides again
            canMoveUp = true; // Make player unableable to move forward
            canMoveDown = false; // Make player unable to move more backwards
            moveDown = false; // The player is no longer moving backward
        }
    }
}
