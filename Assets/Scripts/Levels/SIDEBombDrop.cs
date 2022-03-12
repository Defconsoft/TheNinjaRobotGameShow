using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDEBombDrop : MonoBehaviour
{

    [Header("GameStuff")]
    private GameObject trashCan;



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
    [SerializeField, Range(0,1f)] private float spawnPecentage;

    private void Start() {
        trashCan = GameObject.Find("^TRASH");
        GetSpawnpoints();
        isActive = true;
        
    }

    private void GetSpawnpoints() {
        spawnpoints = GameObject.FindGameObjectsWithTag("SIDESpawn");  
    }


    public void Update() {

       if (isActive){
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnTime) {
                spawnTimer = 0;
                float randomChance = Random.Range (0f, 1f);
                if (randomChance <= spawnPecentage){
                    GameObject clone = Instantiate(Bombs[Random.Range (0, Bombs.Length)], spawnpoints[Random.Range (0, spawnpoints.Length)].transform);
                    clone.transform.parent = trashCan.transform;
                }
                spawnPecentage = spawnPecentage + 0.001f;
            }
        }       

    }


}
