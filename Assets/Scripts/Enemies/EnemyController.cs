using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{


    private GameObject target;
    private NavMeshAgent agent;
    private MeshRenderer meshRender;
    public float damageAmount;
    public GameObject attackEffect, deathEffect;
    private TDShooterGenerator tdGenerator;
    bool alive = true;
    
    void Start()
    {
        //Grab the player as the target
        target = GameObject.Find("Player");  
        agent = GetComponent<NavMeshAgent>();
        meshRender = GetComponent<MeshRenderer>();
        tdGenerator = this.transform.parent.gameObject.transform.GetComponent<TDShooterGenerator>();
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
        Destroy (this.GetComponent<BoxCollider>());
        deathEffect.SetActive (true);
        meshRender.enabled = false;
        tdGenerator.EnemyDied();
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }


    public IEnumerator Attack() {
        alive = false;
        Destroy (agent);
        Destroy (this.GetComponent<BoxCollider>());
        meshRender.enabled = false;
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
