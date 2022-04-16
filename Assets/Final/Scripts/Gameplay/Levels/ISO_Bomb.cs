using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISO_Bomb : MonoBehaviour
{
    public GameObject spawnOrigin;
    private GameObject player;
    private GameManager gameManager;
    public float damageAmount;
    public int scoreAmount;

    private void Start() {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        if (transform.position.y <= -4f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        if (this.tag == "IsoCollectible") {
            if (other.tag == "Player") {
                GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayIsoCollect(false);
                spawnOrigin.GetComponent<ISO_Conveyor>().CoinsCollected++;
                gameManager.AddScore(scoreAmount);
                Destroy(this.gameObject);
            } 
        } else if (this.tag == "IsoPlatform") {
            if (other.tag == "Player") {
                Destroy(this.gameObject);
                GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayIsoCollect(true);
                player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            }
        }
    }
}
