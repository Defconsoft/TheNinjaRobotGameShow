using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDE_FireDamage : MonoBehaviour
{


    private void OnParticleCollision(GameObject other) {
        if (other.tag == "Player") {
            Debug.Log ("OUCH");
        }
    }

}