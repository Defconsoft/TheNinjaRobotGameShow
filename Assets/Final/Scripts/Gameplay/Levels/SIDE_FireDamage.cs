using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDE_FireDamage : MonoBehaviour
{
    public float damageAmount;

    private GameObject player;

    private void Start() {
        player = GameObject.Find("Player");
    }


    private void OnParticleCollision(GameObject other) {
        if (other.tag == "Player") {
            player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
    }

}
