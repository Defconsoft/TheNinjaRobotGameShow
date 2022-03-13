using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDShooterGenerator : MonoBehaviour
{

    private TDManager tdManager;

    [Header("GameStuff")]
    public int numberEnemies;
    public int MaxEnemies;
    public GameObject enemyTypes;
    public float startDelay;
    public bool waveEnded;

    [Header("SpawnerStuff")]
    public Vector3 centre;
    public Vector3 size;
    private GameObject player;
    public int PlayerDistance;
    bool Spawning;
    bool spawnable;
    Vector3 spawnPos;
    public Transform spawnTransform;

    [Header("EnemyTracking")]
    public int enemiesSpawned;
    public int enemiesKilled;
    public int currentTotalEnemies;



    public List<GameObject> enemySpawnpoints = new List<GameObject>();


    private void Start() {
        tdManager = GetComponent<TDManager>();
        player = GameObject.Find("Player");
    }

    public void StartTDGame(){
        FindTheSpawnpoints(this.transform);
        StartCoroutine(StartWave());
    }

    void FindTheSpawnpoints(Transform parent) {

        enemySpawnpoints.Clear();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == "EnemySpawnpoints")
            {
                enemySpawnpoints.Add(child.gameObject);
            }
        }
    }

    //Starts off the enemies
    IEnumerator StartWave () {
        yield return new WaitForSeconds (startDelay);
        Spawning = true;
    }

    //Enemy died function
    public void EnemyDied() {
        currentTotalEnemies--;
        enemiesKilled++;
    }

    private void Update() {
        //spawn the enemies
        if (Spawning){
            while (currentTotalEnemies < MaxEnemies) {
                currentTotalEnemies++;
                enemiesSpawned++;
                while (!spawnable) {
                    spawnPos = (transform.localPosition + centre) + new Vector3 (Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
                    
                    if (Vector3.Distance (spawnPos, player.transform.position) > PlayerDistance){
                        spawnTransform.position = spawnPos;
                        spawnable = true;
                    }
                }
                GameObject clone = Instantiate (enemyTypes, spawnTransform);
                clone.transform.parent = this.transform;

                spawnable = false;
            }
        }
        //stop the spawning
        if (enemiesSpawned == numberEnemies) {
            Spawning = false;
        }
        //stop the game
        if (enemiesKilled >= numberEnemies) {
            //waveEnded = true;
            StartCoroutine(tdManager.FinishTD());
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color (1,0,0,0.5f);
        Gizmos.DrawCube(transform.localPosition + centre, size);   


    }

}
