using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager: MonoBehaviour
{
    public GameObject assignedDoor;
    public int newCamState;
    public int newGameMode;

    public bool switchToOrtho;
    public bool switchToPersp;
    

    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private GameObject mainCamera;


    private void Start() {
        meshCollider = assignedDoor.GetComponent<MeshCollider>();
        meshRenderer = assignedDoor.GetComponent<MeshRenderer>();
        mainCamera = Camera.main.gameObject;
    }


    public void OpenTheDoor() {
        meshCollider.enabled = false;
        meshRenderer.enabled = false;
        GameObject.Find("Player").GetComponent<GameManager>().GameMode = 0;

        mainCamera.GetComponent<CamSwitcher>().camState = newCamState;

        if (switchToOrtho){
            mainCamera.GetComponent<CamSwitcher>().BlendToOrtho();
        } else if (switchToPersp) {
            mainCamera.GetComponent<CamSwitcher>().BlendToPerspective();
        }

        StartCoroutine(GameObject.Find("Player").GetComponent<PlayerMovement>().CamTransition());

        Destroy (this.gameObject, 1.1f);
    }



    public void CloseTheDoor() {
        meshCollider.enabled = true;
        meshRenderer.enabled = true;
        GameObject.Find("Player").GetComponent<GameManager>().GameMode = newGameMode;

        mainCamera.GetComponent<CamSwitcher>().camState = newCamState;

        if (switchToOrtho){
            mainCamera.GetComponent<CamSwitcher>().BlendToOrtho();
        } else if (switchToPersp) {
            mainCamera.GetComponent<CamSwitcher>().BlendToPerspective();
        }

        StartCoroutine(GameObject.Find("Player").GetComponent<PlayerMovement>().CamTransition());

        Destroy (this.gameObject, 1.1f);
    }


    



}
