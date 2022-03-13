using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject bulletPf;
    public GameObject firePoint;

    public bool canShoot = false;
    public float fireRate;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        timer += Time.deltaTime;

        if (canShoot && timer > fireRate){

            timer = 0;
            GameObject bullet = Instantiate (bulletPf, firePoint.transform);
            bullet.transform.parent = GameObject.Find("^TRASH").transform;
        }
    }
}
