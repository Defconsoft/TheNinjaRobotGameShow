using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDE_FireCollect : MonoBehaviour
{

    [Header("GameStuff")]
    private GameObject player;
    public GameObject collectible;
    private GameManager gameManager;
    public int coinsCollected;
    public int coinsToCollect;
    bool FireCollectActive;
    public GameObject EndTrigger;




    [Header("SpawnerStuff")]
    public Vector3 centre;
    public Vector3 size;
    Vector3 spawnPos;
    public Transform spawnTransform;

    [Header("FireStuff")]
    public GameObject preWarnParticle;
    public GameObject flameThrowParticle;
    public Transform[] ventPositions;
    public Transform ventPosition;
    public float FireTimer = 2.0f;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            Destroy (this.GetComponent<BoxCollider>());
            StartSIDEGame();
        }
    }

    void StartSIDEGame(){
        //Put the player in the correct position
        coinsToCollect = gameManager.TotalFireCoins;
        FireCollectActive = true;
        SpawnNextCollect();
    }

    // Update is called once per frame
    void Update()
    {

        

        if (FireCollectActive){
            timer += Time.deltaTime;
            if (coinsCollected == coinsToCollect) {
                FireCollectActive = false;
                EndTrigger.SetActive (true);
            }
        }

        if (timer >= FireTimer){
            int ventChoice = Random.Range(0, ventPositions.Length);
            ventPosition = ventPositions[ventChoice];
            StartCoroutine(SpawnFlames());
            timer = 0f;
        }




    }


    public void SpawnNextCollect() {
        if (coinsCollected < coinsToCollect){
            //Generate the next spawn point
            spawnPos = (transform.localPosition + centre) + new Vector3 (Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            spawnTransform.position = spawnPos;

            GameObject clone = Instantiate (collectible, spawnTransform);
            clone.GetComponent<SIDE_Collectible>().spawnOrigin = this.gameObject;
        }
    }



    IEnumerator SpawnFlames(){
        Transform spawnPoint = ventPosition;
        GameObject preWarn = Instantiate(preWarnParticle, spawnPoint);

        yield return new WaitForSeconds (2f);
        GameObject Fire = Instantiate(flameThrowParticle, spawnPoint);

        yield return new WaitForSeconds (4f);
        Destroy (preWarn);
        Destroy (Fire);
             
    }


    void OnDrawGizmosSelected() {
        Gizmos.color = new Color (1,0,0,0.5f);
        Gizmos.DrawCube(transform.localPosition + centre, size);   
    }


}
