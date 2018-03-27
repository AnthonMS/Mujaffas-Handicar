using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    private float speed;
    private Vector2 _move;

    // Use this for initialization
    void Start()
    {
        _move = transform.position;
        Destroy(gameObject, 2f);
        speed = GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        speed = GameObject.FindGameObjectWithTag("Road").GetComponent<BackgroundManager>().speed;
        _move.y = _move.y - speed * Time.deltaTime;
        transform.position = _move;
    }
}
