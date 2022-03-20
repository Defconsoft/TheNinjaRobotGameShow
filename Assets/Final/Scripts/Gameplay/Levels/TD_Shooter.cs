using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_Shooter : MonoBehaviour
{

    [Header("GameStuff")]
    public int numberEnemies;
    public int MaxEnemies;
    public GameObject enemyTypes;
    public float startDelay;
    public bool inProgress;
    private GameManager gameManager;
    public GameObject EndTrigger;

    [Header("SpawnerStuff")]
    public Vector3 centre;
    public Vector3 size;
    private GameObject player;
    private PlayerShooting playerShooting;
    public int PlayerDistance;
    bool Spawning;
    bool spawnable;
    Vector3 spawnPos;
    public Transform spawnTransform;

    [Header("EnemyTracking")]
    public int enemiesSpawned;
    public int enemiesKilled;
    public int currentTotalEnemies;


    private void Start() {
        player = GameObject.Find("Player");
        playerShooting = player.GetComponent<PlayerShooting>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player"){
            Destroy (this.GetComponent<SphereCollider>());
            StartTDGame();
        }
    }

    public void StartTDGame(){
        numberEnemies = gameManager.TotalShooterEnemies;
        MaxEnemies = gameManager.MaxShooterEnemies;
        inProgress = true;
        playerShooting.canShoot = true;
        StartCoroutine(StartWave());
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

        if (inProgress){
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
                playerShooting.canShoot = false;
                EndTrigger.SetActive (true);
                gameManager.ShooterFinish();
                inProgress = false;
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color (1,0,0,0.5f);
        Gizmos.DrawCube(transform.localPosition + centre, size);   


    }

}

