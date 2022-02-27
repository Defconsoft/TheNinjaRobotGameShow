using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOBomb : MonoBehaviour
{


    private void Update() {
        if (transform.position.y <= -4f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Destroy(this.gameObject);
        }
    }
}
