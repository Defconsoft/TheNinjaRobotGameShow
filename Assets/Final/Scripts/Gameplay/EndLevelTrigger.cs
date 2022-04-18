using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{

    private UIManager uIManager;

    private void Start() {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();  
    }

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Player") {
            Destroy (this.GetComponent<BoxCollider>());
            other.transform.GetComponent<PlayerMovement>().moveVolume = 0;
            uIManager.EndLevelBegin(transform, this.gameObject.transform.parent.transform.GetChild(0).gameObject);
        }
    }
}
