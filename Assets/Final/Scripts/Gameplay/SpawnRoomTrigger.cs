using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomTrigger : MonoBehaviour
{

    private LevelGen levelGen;


    private void Start() {
        levelGen = GameObject.Find("LevelManager").GetComponent<LevelGen>();
    }



    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Player") {
            levelGen.GenerateNextRoom();

            transform.position = new Vector3 (transform.position.x, transform.position.y - 20f, transform.position.z);
           
            Destroy (this.gameObject, 5f);
        }
    }
}
