using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    public GameObject start;
    public GameObject end;

    private BoxCollider2D startColider;
    private BoxCollider2D endColider;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 directionalVector;
    Vector3 elevatorMovement;
    private float vectorX;
    private float vectorY;
    private bool bounced = false;

    void Awake()
    {
        //setting start and end colliders
        setBoundaries(start, end);
        startPos = start.GetComponent<Transform>().position;
        endPos = end.GetComponent<Transform>().position;
        //placing object on start      
        transform.position = startPos;
    }
    void Start()
    {
        //movingDirection
        directionalVector = endPos - startPos;
        float absDifference = Mathf.Abs(directionalVector.x) + Mathf.Abs(directionalVector.y);
        vectorX = directionalVector.x / absDifference;
        vectorY = directionalVector.y / absDifference;
        Debug.Log(vectorX);
        Debug.Log(vectorY);
    }

    private void FixedUpdate()
    {
        float deltaSpeed = speed * Time.deltaTime;
        elevatorMovement = new Vector3(deltaSpeed * vectorX, deltaSpeed * vectorY, 0);
        transform.Translate(elevatorMovement);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "elevatorEnd")
        {
            bounced = true;
            speed = -speed;
        }
        else if (other.tag == "elevatorStart" && bounced)
        {          
                speed = -speed;
        }
    }

    void setBoundaries(GameObject start, GameObject end)
    {
        start.tag = "elevatorStart";
        end.tag = "elevatorEnd";
        Vector2 coliderSize = new Vector2(0.1f, 0.1f);
        startColider = start.AddComponent<BoxCollider2D>() as BoxCollider2D;
        endColider = end.AddComponent<BoxCollider2D>() as BoxCollider2D;
        startColider.isTrigger = true;
        endColider.isTrigger = true;
        startColider.size = coliderSize;
        endColider.size = coliderSize;
        Rigidbody2D startRB = start.AddComponent<Rigidbody2D>() as Rigidbody2D;
        Rigidbody2D endRB = end.AddComponent<Rigidbody2D>() as Rigidbody2D;
        startRB.gravityScale = 0f;
        endRB.gravityScale = 0f;

    }
private void OnTriggerStay2D(Collider2D other) {//moves objects attached
            if (other.tag != "elevatorEnd"&&other.tag != "elevatorStart")
            other.transform.Translate(elevatorMovement);
        }
   }
