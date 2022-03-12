using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeStartTrigger : MonoBehaviour
{

    public bool TopDown, SideOn, Isometric;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
        //Starts the top down game
            if (TopDown) {
                StartCoroutine(this.transform.parent.gameObject.GetComponent<TDManager>().StartTD());
                transform.position = new Vector3 (transform.position.x, transform.position.y - 20f, transform.position.z);
            }

            //Starts the side on game
            if (SideOn) {
                StartCoroutine(this.transform.parent.gameObject.GetComponent<SIDEManager>().StartSIDE());
                transform.position = new Vector3 (transform.position.x, transform.position.y - 20f, transform.position.z);

            }

            //Starts the Isometric game
            if (Isometric){
                StartCoroutine(this.transform.parent.gameObject.GetComponent<ISOManager>().StartISO());
                transform.position = new Vector3 (transform.position.x, transform.position.y - 20f, transform.position.z);
            }

            Destroy (this.gameObject, 5f);
        }

    }

}
