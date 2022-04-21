using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDE_FireDamage : MonoBehaviour
{
    public float damageAmount;

    private GameObject player;

    private void Start() {
        player = GameObject.Find("Player");
        damageAmount = GameObject.Find("GameManager").GetComponent<GameManager>().fireDamage;    
    }


    private void OnParticleCollision(GameObject other) {
        if (other.tag == "Player") {
            GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayFireDamage();
            player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
    }

}
