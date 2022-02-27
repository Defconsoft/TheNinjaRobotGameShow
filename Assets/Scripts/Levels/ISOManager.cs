using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOManager : MonoBehaviour
{
    [Header("GameStuff")]
    private GameObject player;
    private ISOConveyorManager ISOconveyorManager;
    
    public int newCamState;
    public int newGameMode;

    [Header("CamSwitchStuff")]
    private GameObject mainCamera;
    public bool switchToOrtho;
    public bool switchToPersp;

    [Header("EnterDoor")]
    public GameObject EnterDoor;
    private MeshRenderer enterMeshRenderer;
    private MeshCollider enterMeshCollider;

    [Header("EnterDoor")]
    public GameObject ExitDoor;
    private MeshRenderer exitMeshRenderer;
    private MeshCollider exitMeshCollider;

    private void Start() {
        player = GameObject.Find("Player");
        ISOconveyorManager = GetComponent<ISOConveyorManager>();
        enterMeshCollider = EnterDoor.GetComponent<MeshCollider>();
        enterMeshRenderer = EnterDoor.GetComponent<MeshRenderer>();
        exitMeshCollider = ExitDoor.GetComponent<MeshCollider>();
        exitMeshRenderer = ExitDoor.GetComponent<MeshRenderer>();
        mainCamera = Camera.main.gameObject;

    }

    public IEnumerator StartISO() {

        //Close the entrance
        enterMeshCollider.enabled = true;
        enterMeshRenderer.enabled = true;
        GameObject.Find("Player").GetComponent<GameManager>().GameMode = newGameMode;

        //Change the camera
        mainCamera.GetComponent<CamSwitcher>().camState = newCamState;

        mainCamera.GetComponent<CamSwitcher>().BlendToOrtho();

        //Stop the player moving for the transition
        StartCoroutine(player.GetComponent<PlayerMovement>().CamTransition());

        yield return new WaitForSeconds(1.1f);
        ISOconveyorManager.StartISOGame();
    }


    public IEnumerator FinishISO() {

        GameObject.Find("Player").GetComponent<GameManager>().GameMode = 0;

        //Change the camera
        mainCamera.GetComponent<CamSwitcher>().camState = 2;

        mainCamera.GetComponent<CamSwitcher>().BlendToPerspective();

        //Stop the player moving for the transition
        StartCoroutine(player.GetComponent<PlayerMovement>().CamTransition());

        yield return new WaitForSeconds(1.1f);

        //Open the exit door
        exitMeshCollider.enabled = false;
        exitMeshRenderer.enabled = false;
    }

}
