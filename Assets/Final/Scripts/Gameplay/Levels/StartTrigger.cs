using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartTrigger : MonoBehaviour
{
    public GameObject managerObject;
    private GameObject player;
    public Transform moveObject;
    private Vector3 movePosition;
    public GameObject StartUI;
    public bool TopDown, Iso, Side;


    private void Start() {
        player = GameObject.Find("Player");
        movePosition = moveObject.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            Destroy (this.GetComponent<BoxCollider>());
            StartCoroutine(MovePlayer());
        }
    }

    public IEnumerator MovePlayer(){
        player.GetComponent<PlayerMovement>().canMove = false;
        player.transform.DOMove(movePosition, 3);
        StartUI.SetActive (true);
        yield return new WaitForSeconds(4f);
        player.GetComponent<PlayerMovement>().canMove = true;
        yield return new WaitForSeconds(1f);
        Destroy(StartUI);

        if (TopDown){    
            managerObject.GetComponent<TD_Shooter>().StartTDGame();
        }

        if (Iso){
            managerObject.GetComponent<ISO_Conveyor>().StartISOGame();
        }

        if (Side){
            managerObject.GetComponent<SIDE_FireCollect>().StartSIDEGame();
        }


    }

}
