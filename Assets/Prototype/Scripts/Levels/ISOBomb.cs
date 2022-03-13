using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOBomb : MonoBehaviour
{
    public GameObject spawnOrigin;

    private void Update() {
        if (transform.position.y <= -4f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        if (this.tag == "IsoCollectible") {
            if (other.tag == "Player") {
                spawnOrigin.GetComponent<ISOConveyorManager>().CoinsCollected++;
                Destroy(this.gameObject);
            } 
        } else if (this.tag == "IsoPlatform") {
            if (other.tag == "Player") {
                Destroy(this.gameObject);
                //Damage the player
            }
        }
    }
}
