using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISO_Conveyor : MonoBehaviour
{
    [Header("FallAway")]
    public GameObject FallAway;
    public float lerpTime;
    float currentLerpTime;
    private float perc = 0f;
    public float moveDistance = 1f;
    bool removeFallAway = false;
    Vector3 startPos;
    Vector3 endPos;

    [Header("EndAway")]
    public GameObject EndAway;
    private float perc2 = 0f;
    bool removeEndAway = false;
    Vector3 startPosEnd;
    Vector3 endPosEnd;

    [Header("ConveyorStuff")]
    public GameObject conBelt;
    private ISO_ConveyorBelt conBeltComp;

    [Header("BombStuff")]
    public GameObject[] Bombs;
    public GameObject[] spawnpoints;

    [Header("GameStuff")]
    bool isActive;
    bool isFinished;
    public int CoinsToCollect;
    public int CoinsCollected;
    public float spawnTime;
    private float spawnTimer;
    private GameObject trashCan;
    private GameManager gameManager;
    [SerializeField, Range(0,1f)] private float spawnPecentage;


    private void Start() {
        startPos = FallAway.transform.position;
        endPos = FallAway.transform.position - FallAway.transform.up * moveDistance;
        startPosEnd = EndAway.transform.position;
        endPosEnd = EndAway.transform.position + EndAway.transform.up * moveDistance;
        conBeltComp = conBelt.GetComponent<ISO_ConveyorBelt>();
        trashCan = GameObject.Find("^TRASH");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log ("FIRE");
        if (other.tag == "Player"){
            Destroy (this.GetComponent<BoxCollider>());
            StartISOGame();
        }
    }


    public void StartISOGame() {
        CoinsToCollect = gameManager.TotalCoins;
        removeFallAway = true;
        conBeltComp.active = true;
    }


    public void Update() {

        if (removeFallAway) {
            RemoveFallAway();
        }

        if (removeEndAway){
            RemoveEndAway();
        }

        if (perc >= 1.0) {
            perc = 0;
            removeFallAway = false;
            Destroy(FallAway, 0.1f);
            isActive = true;
            
        }

        if (perc2 >= 1.0) {
            perc2 = 0;
            removeEndAway = false;
        }

        //the game is running
        if (isActive){
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnTime) {
                spawnTimer = 0;
                float randomChance = Random.Range (0f, 1f);
                if (randomChance <= spawnPecentage){
                    GameObject clone = Instantiate(Bombs[Random.Range (0, Bombs.Length)], spawnpoints[Random.Range (0, spawnpoints.Length)].transform);
                    clone.GetComponent<ISO_Bomb>().spawnOrigin = this.gameObject;
                    clone.transform.parent = trashCan.transform;
                }
                spawnPecentage = spawnPecentage + 0.001f;
            }

            if (CoinsCollected == CoinsToCollect) {
                    if (!isFinished) {
                    isActive = false;
                    foreach (Transform child in trashCan.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    isFinished = true;
                    currentLerpTime = 0;
                    removeEndAway = true;
                    gameManager.ConveyorFinish();
                }
            
            }

        }


        

    }
    
    void RemoveFallAway(){
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime) {
            currentLerpTime = lerpTime;
        }
 
        //lerp!
        perc = currentLerpTime / lerpTime;
        FallAway.transform.position = Vector3.Lerp(startPos, endPos, perc);
    }

    void RemoveEndAway(){
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime) {
            currentLerpTime = lerpTime;
        }
 
        //lerp!
        perc2 = currentLerpTime / lerpTime;
        EndAway.transform.position = Vector3.Lerp(startPosEnd, endPosEnd, perc2);
    }

}
