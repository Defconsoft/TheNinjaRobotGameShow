using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDE_Collectible : MonoBehaviour
{
    public GameObject spawnOrigin;
    private GameManager gameManager;
    public int scoreAmount;

    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


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
                gameManager.AddScore(scoreAmount);
                spawnOrigin.GetComponent<SIDE_FireCollect>().SpawnNextCollect();
                Destroy(this.gameObject);
            } 
        } 
    }
}
