using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Image HealthFill;
    public float Health = 1f;
    public GameObject DeathContainer;
    public CharacterController playerController;
    public GameObject playerModel;
    private GameObject player;

    private void Start() {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        HealthFill.fillAmount = Health;

        if (Health <= 0) {
            Death();
        }

    }


    public void TakeDamage(float damageAmount){
        Health = Health - damageAmount;
    }



    public void Death(){
        player.GetComponent<PlayerMovement>().canMove = false;
        player.GetComponent<PlayerShooting>().canShoot = false;
        playerController.enabled = false;
        playerModel.SetActive(false);
        DeathContainer.SetActive(true);

    }



}
