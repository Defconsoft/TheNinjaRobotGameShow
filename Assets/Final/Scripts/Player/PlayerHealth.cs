using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{

    public Image HealthFill;
    public float Health = 1f;
    public GameObject DeathContainer;
    public CharacterController playerController;
    public GameObject playerModel;
    private GameObject player;
    private GameManager gameManager;
    private UIManager uIManager;
    private CamSwitcher camSwitcher;
    public GameObject DeathCanvas;
    public GameObject hitParticle;
    public GameObject trashCan;
    public bool Dead;
    public bool HitEffect;

    private void Start() {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        camSwitcher = Camera.main.gameObject.GetComponent<CamSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthFill.fillAmount = Health;

        if (Health <= 0) {
            if (!Dead){
                Dead = true;
                Death();
            }
        }

        //player death from falling
        if (transform.position.y <= -6f) {
            Health = 0;
        }

    }


    public void TakeDamage(float damageAmount){
        Health = Health - damageAmount;
        if (!HitEffect){
            StartCoroutine(PlayHitEffect());
        }

    }

    IEnumerator PlayHitEffect(){
        HitEffect = true;
        SpawnHitParticle();
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayPlayerHit(this.transform.position);
        yield return new WaitForSeconds(0.3f);
        HitEffect = false;
    }

    public void SpawnHitParticle(){
        GameObject clone = Instantiate(hitParticle, transform);
        Destroy(clone, 1f);
    }



    public void Death(){
        camSwitcher.BlendToPerspective();
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayEnemyDeath(this.transform.position);
        uIManager.InGameMoveOut();
        player.transform.DORotate(new Vector3 (0,0,0), 1f);
        player.GetComponent<PlayerMovement>().canMove = false;
        player.GetComponent<PlayerShooting>().canShoot = false;
        playerController.enabled = false;
        playerModel.SetActive(false);
        DeathContainer.SetActive(true);
        StartCoroutine(gameManager.HandleDeath());
        GameObject clone = Instantiate (DeathCanvas,transform);
    }



}
