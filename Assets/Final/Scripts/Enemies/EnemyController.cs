using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{


    private GameObject target;
    private GameManager gameManager;
    private NavMeshAgent agent;
    public GameObject enemyModel;
    public float damageAmount;
    public int scoreAmount;
    public GameObject attackEffect, deathEffect;
    public GameObject spawnOrigin;
    private TD_Shooter tdGenerator;
    bool alive = true;



    
    void Start()
    {
        //Grab the player as the target
        target = GameObject.Find("Player");  
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        tdGenerator = spawnOrigin.transform.GetComponent<TD_Shooter>();
        RandomiseTheEnemy();
    }

    public void RandomiseTheEnemy() {
        float offset;
        offset = Random.Range (0,32) * 0.03f;
        foreach (Transform child in enemyModel.transform)
        {
            child.GetComponent<MeshRenderer>().material.mainTextureOffset =  new Vector2(offset, 0);
        }
    }
    
    
    void Update()
    {
        if (alive) {
            agent.destination = target.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            EnemyAttack();
        }
    }

    public IEnumerator Death() {
        alive = false;
        Destroy (agent);
        Destroy (this.GetComponent<CapsuleCollider>());
        deathEffect.SetActive (true);
        gameManager.AddScore (scoreAmount);
        Destroy(enemyModel);
        tdGenerator.EnemyDied();
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }


    public IEnumerator Attack() {
        alive = false;
        Destroy (agent);
        Destroy (this.GetComponent<CapsuleCollider>());
        Destroy(enemyModel);
        target.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        attackEffect.SetActive (true);
        tdGenerator.EnemyDied();
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }
    void EnemyAttack()
    {
        //target.GetComponent<FPS_PlayerHealth>().playerHealth -= damageAmount;
        StartCoroutine("Attack");
    }

}
