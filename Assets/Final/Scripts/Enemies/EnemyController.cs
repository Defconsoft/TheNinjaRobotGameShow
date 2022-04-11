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
    private TD_Shooter tdGenerator;
    bool alive = true;



    
    void Start()
    {
        //Grab the player as the target
        target = GameObject.Find("Player");  
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        tdGenerator = this.transform.parent.gameObject.transform.GetComponent<TD_Shooter>();
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
