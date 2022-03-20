using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISO_ConveyorBelt : MonoBehaviour
{
    public GameObject belt;
    public Transform endPoint;
    public float speed;

    private BoxCollider moveTrigger;
    public bool active;

    public void Start(){
        moveTrigger = GetComponent<BoxCollider>();
    }

    public void Update(){
        if (active) {
            moveTrigger.enabled = true;
        }
    }

    public void OnTriggerStay(Collider other) {

            if (other.tag == "IsoPlatform" || other.tag == "IsoCollectible"){
                Vector3 moveZ = new Vector3 (other.transform.position.x, other.transform.position.y, endPoint.position.z);
                other.transform.position = Vector3.MoveTowards(other.transform.position, moveZ, speed * Time.deltaTime);
            }
    }
}
