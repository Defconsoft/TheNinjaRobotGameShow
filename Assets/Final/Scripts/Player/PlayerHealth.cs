using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Image HealthFill;
    public float Health = 1f;


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


    }



}
