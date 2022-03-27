using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOConveyorManager : MonoBehaviour
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

    [Header("ConveyorStuff")]
    public GameObject conBelt;
    private ISOConveyorBelt conBeltComp;

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
    private ISOManager manager;
    [SerializeField, Range(0,1f)] private float spawnPecentage;


    private void Start() {
        startPos = FallAway.transform.position;
        endPos = FallAway.transform.position - transform.up * moveDistance;
        conBelt = transform.Find("Platform").gameObject;
        manager = GetComponent<ISOManager>();
        conBeltComp = conBelt.GetComponent<ISOConveyorBelt>();
        trashCan = GameObject.Find("^TRASH");
        GetSpawnpoints();
        
    }

    private void GetSpawnpoints() {
        spawnpoints = GameObject.FindGameObjectsWithTag("ISOSpawn");  
    }


    public void StartISOGame() {
        removeFallAway = true;
        conBeltComp.active = true;
    }


    public void Update() {

        if (removeFallAway) {
            RemoveFallAway();
        }

        if (perc >= 1.0) {
            perc = 0;
            removeFallAway = false;
            Destroy(FallAway, 0.1f);
            isActive = true;
            
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
        }

        if (CoinsCollected == CoinsToCollect) {
            if (!isFinished) {
                isActive = false;
                foreach (Transform child in trashCan.transform)
                {
                    Destroy(child.gameObject);
                }
                StartCoroutine(manager.FinishISO());
                isFinished = true;
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

}
