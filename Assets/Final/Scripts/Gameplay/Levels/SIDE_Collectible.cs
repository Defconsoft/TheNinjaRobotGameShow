using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDE_Collectible : MonoBehaviour
{
    public GameObject spawnOrigin;

    private void Update() {
        if (transform.position.y <= -4f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        if (this.tag == "SideCollectible") {
            if (other.tag == "Player") {
                spawnOrigin.GetComponent<SIDE_FireCollect>().coinsCollected ++;
                spawnOrigin.GetComponent<SIDE_FireCollect>().SpawnNextCollect();
                Destroy(this.gameObject);
            } 
        } 
    }
}
