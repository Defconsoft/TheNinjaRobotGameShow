using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDEMovingPlatform : MonoBehaviour
{

    private Vector3 startPosition;
    private Vector3 nextPosition;
    private Vector3 currentTarget;

    private float speed;
    public bool canMove;
    private bool up = true;
    private bool moveComplete;

    float minSpeed = 2f;
    float maxSpeed = 4f;

    float minTime = 2f;
    float maxTime = 4f;

    float waitTime;
    float moveChance;



    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        StartCoroutine(moveTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (canMove) {
            if (up){
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
            } else {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
            }

            if (Vector3.Distance(transform.position, currentTarget) < 0.001f)
            {
                canMove = false;
                transform.position = currentTarget;
                moveComplete = true;
                
            }
        } 

        if (moveComplete == true) {
            //Sort the direction.
            if (up) {
                up = false;
            } else {
                up = true;
            }

            StartCoroutine(moveTimer());
        }

    }

    IEnumerator moveTimer(){
        moveComplete = false;
        SetCurrentTarget();
        SetSpeed();
        waitTime = Random.Range (minTime, maxTime);
        yield return new WaitForSeconds(waitTime);
        SetCanMove();
    }


    void SetCurrentTarget(){
        if (up){
            nextPosition = new Vector3 (startPosition.x, startPosition.y + Random.Range (1f, 5f), startPosition.z);
            currentTarget = nextPosition;
        } else {
            currentTarget = startPosition;
        }

    }

    void SetSpeed(){
        speed = Random.Range (minSpeed, maxSpeed);
    }

    void SetCanMove(){
        moveChance = Random.Range (0,1f);
        if (moveChance > 0.6f) {
            canMove = true;
        } else {
            canMove = false;
            StartCoroutine(moveTimer());
        }
    }


}
