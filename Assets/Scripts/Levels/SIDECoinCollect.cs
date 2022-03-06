using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDECoinCollect : MonoBehaviour
{

    [Header("GameStuff")]
    public GameObject movingPlatform;
    public GameObject rightPlatformSpawn, leftPlatformSpawn;
    private GameObject trashCan;


    private void Start() {
        trashCan = GameObject.Find("^TRASH");




        GameObject clone = Instantiate (movingPlatform, rightPlatformSpawn.transform);
        clone.GetComponent<MovingPlatform>().SetPlatformTarget(1);
        clone.transform.parent = trashCan.transform;
        
    }



    public void Update() {

       

    }


}
