using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public GameObject arrows;
    private UIManager uIManager;
    public GameObject CanvasPanel;
    public Text InGameAssist;
    private GameObject trashCan;

    [Header("UIStuff")]
    public Image WinBG;
    public Image You, Win;

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
    private List<Coroutine> routines = new List<Coroutine>(); 




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        trashCan = GameObject.Find("^TRASH");
    }

    public void StartSIDEGame(){
        //Put the player in the correct position
        CanvasPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -544f), 1.5f);
        uIManager.InGameMoveIn();
        coinsToCollect = gameManager.TotalFireCoins;
        FireCollectActive = true;
        SpawnNextCollect();
    }

    // Update is called once per frame
    void Update()
    {

        InGameAssist.text = (coinsToCollect - coinsCollected).ToString() + " COINS TO COLLECT";

        if (FireCollectActive){
            timer += Time.deltaTime;
            if (coinsCollected == coinsToCollect) {
                FireCollectActive = false;
                player.GetComponent<PlayerMovement>().canMove = false;
                StartCoroutine(EndSIDEGame());
                foreach (Transform child in trashCan.transform) {
                    Destroy(child.gameObject);
                    //child.transform.position = new Vector3(-300, child.transform.position.y, child.transform.position.z);
                }
                gameManager.GameFinish();
                EndTrigger.SetActive (true);
            }
        }
        if (player.GetComponent<PlayerHealth>().Dead == true && FireCollectActive == true){
            FireCollectActive = false;
            CanvasPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -705f), 1.5f);
        }

        if (timer >= FireTimer){
            int ventChoice = Random.Range(0, ventPositions.Length);
            ventPosition = ventPositions[ventChoice];
            StartCoroutine(SpawnFlames());
            timer = 0f;
        }




    }

    private IEnumerator EndSIDEGame(){
        CanvasPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -705f), 1.5f);
        uIManager.InGameMoveOut();
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayCrowdHappy(player.transform.position);
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().YouWin();

        var sequence = DOTween.Sequence();
        sequence.Append(WinBG.GetComponent<RectTransform>().DOScale(new Vector3 (1, 1, 1), 1.5f));
        sequence.AppendInterval(3f);
        sequence.Append(WinBG.GetComponent<RectTransform>().DOScale(new Vector3 (0, 0, 0), 0.5f));

        var sequence1 = DOTween.Sequence();

        sequence1.Append(You.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, 104f), 1.5f).SetEase(Ease.OutQuart));
        sequence1.AppendInterval(1f);
        sequence1.Append(You.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, 694f), 1.5f).SetEase(Ease.InQuart));

        var sequence2 = DOTween.Sequence();

        sequence2.Append(Win.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -122f), 1.5f).SetEase(Ease.OutQuart));
        sequence2.AppendInterval(1f);
        sequence2.Append(Win.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -694f), 1.5f).SetEase(Ease.InQuart));

        yield return new WaitForSeconds(5f);
        player.GetComponent<PlayerMovement>().canMove = true;
        arrows.SetActive (true);

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
        preWarn.transform.parent = trashCan.transform ;
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayBurner(preWarn.transform.position);
        yield return new WaitForSeconds (2f);
        GameObject Fire = Instantiate(flameThrowParticle, spawnPoint);
        Fire.transform.parent = trashCan.transform ;
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayFlames(Fire.transform.position);
        yield return new WaitForSeconds (4f);
        Destroy (preWarn);
        Destroy (Fire);
             
    }


    void OnDrawGizmosSelected() {
        Gizmos.color = new Color (1,0,0,0.5f);
        Gizmos.DrawCube(transform.localPosition + centre, size);   
    }


}
