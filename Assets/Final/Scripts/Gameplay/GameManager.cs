using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    
    [Header ("Game States")] 
    public int GameMode; //0 - Traversal, 1 - TopDownShooter, 2 - Side On, 3 - Isometric, 4 - UI, 5 - Title
    private GameObject mainCamera;
    private CamSwitcher camSwitcher;
    private GameObject player;


    [Header ("Shooter Variables")] 
    public int TotalShooterEnemies;
    public int MaxShooterEnemies;
    public int EnemiesAddPerLevel;

    [Header ("Conveyor Variables")] 
    public int TotalCoins;
    public int CoinsToAdd;

    [Header ("FireCollect Variables")] 
    public int TotalFireCoins;
    public int FireCoinsToAdd;

    [Header ("Other Variables")]
    private UIManager uiManager;
    public int Score;

    [Header("Lights")]
    public GameObject PlayerLight;


    private void Start() {
        camSwitcher = Camera.main.gameObject.GetComponent<CamSwitcher>();
        camSwitcher.camState = 5;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        player = GameObject.Find("Player");
        //Turn on the title screen
        uiManager.TitleScreen.enabled = true;
        StartCoroutine(StartMainGame());
    }

    private void Update() {

        //player death from falling
        if (transform.position.y <= -4f) {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }

    }


    public void ShooterFinish(){
        TotalShooterEnemies = TotalShooterEnemies + EnemiesAddPerLevel;
    }

    public void ConveyorFinish(){
        TotalCoins = TotalCoins + CoinsToAdd;
    }

    public void FireCollectFinish(){
        TotalFireCoins = TotalFireCoins + FireCoinsToAdd;
    }


    IEnumerator StartMainGame() {
        yield return new WaitForSeconds (2f);
        StartCoroutine(uiManager.Countdown());
    }

    public void LightsToPlayer(){

        PlayerLight.GetComponent<Rigidbody>().DOLookAt(player.transform.position, 2f, AxisConstraint.None, Vector3.up); 

    }

    public void AddScore(int scoreAmount){
        Score = Score + scoreAmount;
    }




}
