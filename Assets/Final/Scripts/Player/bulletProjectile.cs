using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public float speed = 10f;

    private void Awake() {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        bulletRigidbody.velocity = transform.forward * speed;
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayLaserShot(this.transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);

        if (other.tag == "Enemy") {
            GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayShootSpeech(true);
            StartCoroutine(other.gameObject.GetComponent<EnemyController>().Death());
        }
    }


}
