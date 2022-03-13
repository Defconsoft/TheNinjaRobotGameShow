using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDEBomb : MonoBehaviour
{

    private void Update() {
        if (transform.position.y <= -4f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
    
        if (other.tag == "Player") {
            Destroy(this.gameObject);
        } else if (other.tag == "SIDEPlatform") {
            Destroy(this.gameObject);
        }
    }
}
