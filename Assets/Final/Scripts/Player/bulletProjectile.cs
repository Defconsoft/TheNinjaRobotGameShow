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
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);

        if (other.tag == "Enemy") {
            StartCoroutine(other.gameObject.GetComponent<EnemyController>().Death());
        }
    }


}
