using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public GameObject CanvasPanel;
    public Text InGameAssist;
    private GameObject trashcan;
    public GameObject arrows;
    private UIManager uIManager;

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

    [Header("UIStuff")]
    public Image WinBG;
    public Image You, Win;


    private void Start() {
        player = GameObject.Find("Player");
        playerShooting = player.GetComponent<PlayerShooting>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        trashcan = GameObject.Find("^TRASH");
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }


    public void StartTDGame(){
        CanvasPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -515f), 1.5f);
        numberEnemies = gameManager.TotalShooterEnemies;
        MaxEnemies = gameManager.MaxShooterEnemies;
        inProgress = true;
        playerShooting.canShoot = true;
        uIManager.InGameMoveIn();
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
        
        InGameAssist.text = (numberEnemies - enemiesKilled).ToString() + " ENEMIES TO KILL";

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
                    clone.GetComponent<EnemyController>().spawnOrigin = this.gameObject;
                    clone.transform.parent = trashcan.transform;

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
                player.GetComponent<PlayerMovement>().canMove = false;
                inProgress = false;
                StartCoroutine(EndTDGame());
            }

            if (player.GetComponent<PlayerHealth>().Dead == true && inProgress == true){
                inProgress = false;
                CanvasPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -705f), 1.5f);
                foreach (Transform child in trashcan.transform) {
                     Destroy(child.gameObject);
                }
            }
        }
    }

    private IEnumerator EndTDGame(){
        CanvasPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -705f), 1.5f);
        uIManager.InGameMoveOut();
        foreach (Transform child in trashcan.transform) {
            Destroy(child.gameObject);
        }
        
        var sequence = DOTween.Sequence();
        sequence.Append(WinBG.GetComponent<RectTransform>().DOScale(new Vector3 (1, 1, 1), 1.5f));
        sequence.AppendInterval(3f);
        sequence.Append(WinBG.GetComponent<RectTransform>().DOScale(new Vector3 (0, 0, 0), 0.5f));

        var sequence1 = DOTween.Sequence();

        sequence1.Append(You.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, 104f), 1.5f).SetEase(Ease.OutQuart));
        sequence1.AppendInterval(1f);
        sequence1.Append(You.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, 694f), 1.5f).SetEase(Ease.InQuart));

        var sequence2 = DOTween.Sequence();

        sequence2.Append(Win.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -122f), 1.5f).SetEase(Ease.OutQuart));
        sequence2.AppendInterval(1f);
        sequence2.Append(Win.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -694f), 1.5f).SetEase(Ease.InQuart));

        yield return new WaitForSeconds(5f);
        player.GetComponent<PlayerMovement>().canMove = true;
        arrows.SetActive (true);

    }

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color (1,0,0,0.5f);
        Gizmos.DrawCube(transform.localPosition + centre, size);   


    }

}

