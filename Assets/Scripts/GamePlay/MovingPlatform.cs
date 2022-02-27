using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float rightLimit;
    public float leftLimit;
    public float speed = 2.0f;
    private int direction = 1;
    private float realRightLimit;
    private float realLeftLimit;
    
    private void Start() {
        realRightLimit = transform.position.z + rightLimit;
        realLeftLimit = transform.position.z - leftLimit;
    }    



    void Update () {
        if (transform.position.z > realRightLimit) {
            direction = -1;
        }
        else if (transform.position.z < realLeftLimit) {
            direction = 1;
        }
        Vector3 movement = Vector3.forward * direction * speed * Time.deltaTime; 
        transform.Translate(movement); 
    }
}