using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISO_Bomb : MonoBehaviour
{
    public GameObject spawnOrigin;
    private GameObject player;
    private GameManager gameManager;
    public GameObject explosionParticle;
    public GameObject trashCan;
    public float damageAmount;
    public int scoreAmount;
    public bool isCoin;

    private void Start() {
        trashCan = GameObject.Find("^TRASH");
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (isCoin){
            GameObject.Find("SoundManager").GetComponent<SFXManager>().PlaySpawnCoin(transform.position);
        } else {
            GameObject.Find("SoundManager").GetComponent<SFXManager>().PlaySpawnBox(transform.position);
        }
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
                GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayCollectCoin(transform.position);
                spawnOrigin.GetComponent<ISO_Conveyor>().CoinsCollected++;
                gameManager.AddScore(scoreAmount);
                Destroy(this.gameObject);
            } 
        } else if (this.tag == "IsoPlatform") {
            if (other.tag == "Player") {
                GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayEnemyDeath(this.transform.position);
                GameObject clone = Instantiate(explosionParticle, transform);
                clone.transform.parent = trashCan.transform;
                Destroy(clone, 3f);
                GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayIsoCollect(true);
                player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);

                Destroy(this.gameObject);

            }
        }
    }
}
