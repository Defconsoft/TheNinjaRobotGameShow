using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCleaner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Level") {
            Debug.Log("HitSomething");
            Destroy(other.gameObject);
        }
    }
}
