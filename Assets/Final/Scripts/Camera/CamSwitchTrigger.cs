using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchTrigger : MonoBehaviour
{

    public bool Traversal, TopDown, SideOn, Isometric;

    private GameManager gameManager;
    private CamSwitcher camSwitcher;

    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        camSwitcher = Camera.main.gameObject.GetComponent<CamSwitcher>();
    }


    private void OnTriggerEnter(Collider other) {
 
        if (other.tag == "Player") {
        //Starts the top down game
            if (Traversal){
                gameManager.GameMode = 0;
                camSwitcher.camState = 0;
                camSwitcher.BlendToPerspective();
                transform.position = new Vector3 (transform.position.x, transform.position.y - 20f, transform.position.z);
            }


            if (TopDown) {
                gameManager.GameMode = 1;
                camSwitcher.camState = 1;
                camSwitcher.BlendToPerspective();
                transform.position = new Vector3 (transform.position.x, transform.position.y - 20f, transform.position.z);
            }

            //Starts the side on game
            if (SideOn) {
                gameManager.GameMode = 2;
                camSwitcher.camState = 2;
                camSwitcher.BlendToOrtho();
                transform.position = new Vector3 (transform.position.x, transform.position.y - 20f, transform.position.z);

            }

            //Starts the Isometric game
            if (Isometric){
                gameManager.GameMode = 3;
                camSwitcher.camState = 3;
                camSwitcher.BlendToOrtho();
                transform.position = new Vector3 (transform.position.x, transform.position.y - 20f, transform.position.z);
            }

            Destroy (this.gameObject, 5f);
        }

    }
}
