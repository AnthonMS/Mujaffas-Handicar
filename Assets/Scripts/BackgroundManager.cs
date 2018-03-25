using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    private float bgVerticalHeight;
    //private float bgHorizontalLength;
    public float speed = 5f;
    public float orgSpeed;

    // Use this for initialization
    void Start ()
    {
        bgVerticalHeight = GetComponent<Renderer>().bounds.size.y;
        //bgHorizontalLength = GetComponent<Renderer>().bounds.size.x;
        orgSpeed = speed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(new Vector2(0, -speed * Time.deltaTime));

        float dist = (transform.position - Camera.main.transform.position).z;
        //float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        //float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float Bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        //float Top = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        //Debug.Log("Left: " + leftBorder + ", Right: " + rightBorder + ", Bottom: " + Bottom + ", Top: " + Top);

        if (Bottom > transform.position.y + (bgVerticalHeight / 2))
        {
            //Debug.Log("BG OUT OF SIGHT!");
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        Vector2 bgOffset = new Vector2(0, (bgVerticalHeight * 2f) - 0.1f);
        transform.position = (Vector2)transform.position + bgOffset;
    }
}
