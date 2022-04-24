using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header ("DEBUG")]
    public bool Testing; 
    
    [Header ("Game States")] 
    public int GameMode; //0 - Traversal, 1 - TopDownShooter, 2 - Side On, 3 - Isometric, 4 - UI, 5 - Title, 6 - Death
    private GameObject mainCamera;
    private CamSwitcher camSwitcher;
    private GameObject player;
    private PlayerInput playerInput;

    [Header ("Shooter Variables")] 
    public int TotalShooterEnemies;
    public int MaxShooterEnemies;
    public int EnemiesAddPerLevel;
    public float fireRateIncrease;

    [Header ("Conveyor Variables")] 
    public int TotalCoins;
    public int CoinsToAdd;
    public float conveySpeed;
    public float speedIncrease;

    [Header ("FireCollect Variables")] 
    public int TotalFireCoins;
    public int FireCoinsToAdd;
    public float fireDamageIncrease;
    public float fireDamage;

    [Header ("Other Variables")]
    public int Score;
    private UIManager uiManager;


    [Header("Lights")]
    public GameObject PlayerLight;


    private void Start() {
        camSwitcher = Camera.main.gameObject.GetComponent<CamSwitcher>();
        camSwitcher.camState = 5;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        player = GameObject.Find("Player");
        //Turn on the title screen
        if (Testing){
            uiManager.TitleScreen.enabled = true;
            StartCoroutine(StartMainGame());
        } else {
            
            if (GameObject.Find("DND").GetComponent<ReplayHolder>().hasPlayed){
                uiManager.StartGameAgain();
            } else {
                uiManager.TitleScreen.enabled = true;
                StartCoroutine(StartMainGame());
                GameObject.Find("DND").GetComponent<ReplayHolder>().hasPlayed = true;
            }
        }
        DOTween.SetTweensCapacity(1250,50);


        playerInput = GetComponent<PlayerInput>();
    }

    private void Update() {
        if (Keyboard.current.rKey.wasPressedThisFrame){
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);    
        }

        if (Keyboard.current.gKey.wasPressedThisFrame){
            GameObject.Find("LevelManager").GetComponent<LevelGen>().GenerateMainRoom();   
        }      
    }


    public void GameFinish(){
        TotalShooterEnemies = TotalShooterEnemies + EnemiesAddPerLevel;
        TotalCoins = TotalCoins + CoinsToAdd;
        TotalFireCoins = TotalFireCoins + FireCoinsToAdd;
    }

    public void ShooterFinish(){
        player.GetComponent<PlayerShooting>().fireRate = player.GetComponent<PlayerShooting>().fireRate + fireRateIncrease;
        if (MaxShooterEnemies < 10) {
            MaxShooterEnemies = MaxShooterEnemies++;
        }
    }

    public void ConveyorFinish(){
        conveySpeed = conveySpeed + speedIncrease;
    }

    public void GrabFinish(){
        fireDamage = fireDamage + fireDamageIncrease;
    }



    IEnumerator StartMainGame() {
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().EpisodeStartSpeech1();
        yield return new WaitForSeconds (4f);
        StartCoroutine(uiManager.Countdown());
    }


    public void AddScore(int scoreAmount){
        Score = Score + scoreAmount;
    }

    public IEnumerator HandleDeath(){
        yield return new WaitForSeconds(2f);
        camSwitcher.camState = 6;
        GameMode = 6;
        yield return new WaitForSeconds(2f);
    }


}
