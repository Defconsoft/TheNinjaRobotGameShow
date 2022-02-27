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
    public float spawnTime;
    private float spawnTimer;
    [SerializeField, Range(0,1f)] private float spawnPecentage;


    private void Start() {
        startPos = FallAway.transform.position;
        endPos = FallAway.transform.position - transform.up * moveDistance;
        conBelt = transform.Find("Platform").gameObject;
        conBeltComp = conBelt.GetComponent<ISOConveyorBelt>();
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
            removeFallAway = false;
            Destroy(FallAway, 0.1f);
            isActive = true;
            
        }

        //the game is running
        if (isActive){
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnTime) {
                spawnTimer = 0;
                for (int i = 0; i < spawnpoints.Length; i++)
                {
                    float randomChance = Random.Range (0f, 1f);
                    if (randomChance <= spawnPecentage){
                        GameObject clone = Instantiate(Bombs[Random.Range (0, Bombs.Length)], spawnpoints[i].transform);
                        clone.transform.parent = GameObject.Find("^TRASH").transform;
                    }
                }
                spawnPecentage = spawnPecentage + 0.01f;
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
