using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplosion : MonoBehaviour
{

    public float radius = 1.0F;
    public float power = 20.0F;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 explosionPos = transform.position - offset;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }
}
