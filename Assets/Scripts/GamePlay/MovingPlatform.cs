using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  
    public float speed;
    bool left;


    public void SetPlatformTarget(int direction){
        if (direction != 0){
            left = true;
        } 
    }

    private void FixedUpdate() {
        if (!left){
            transform.Translate(0,0,Time.deltaTime * speed);
        } else {
            transform.Translate(0,0,-Time.deltaTime * speed);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
          other.transform.parent = this.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
          other.transform.parent = GameObject.Find("@Player").transform;
        }
    }


}


  /*
    public float rightLimit;
    public float leftLimit;
    public float speed = 2.0f;
    private int direction = 1;
    private float realRightLimit;
    private float realLeftLimit;
    
    private void Start() {
        realRightLimit = transform.position.x + rightLimit;
        realLeftLimit = transform.position.x - leftLimit;
    }    



    void Update () {
        if (transform.position.x > realRightLimit) {
            direction = -1;
        }
        else if (transform.position.x < realLeftLimit) {
            direction = 1;
        }
        Vector3 movement = Vector3.right * direction * speed * Time.deltaTime; 
        transform.Translate(movement); 
    }
}*/