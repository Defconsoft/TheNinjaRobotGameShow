using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{

    private UIManager uIManager;
    public Transform movePoint;

    private void Start() {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();  
    }

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Player") {
            Destroy (this.GetComponent<BoxCollider>());
            uIManager.EndLevelBegin(movePoint, this.gameObject.transform.parent.transform.GetChild(0).gameObject);
        }
    }
}
