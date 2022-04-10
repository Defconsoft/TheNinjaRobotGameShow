using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private CamSwitcher camSwitcher;
    private GameManager gameManager;
    private LevelGen levelGen;
    private GameObject player;
    private PlayerMovement playerMovement;


    [Header("TitleScreen")]
    public Canvas TitleScreen;
    public Text CountDownBox;
    public GameObject titleMonitor;
    public GameObject titleLogo;
    private int timer = 5;
    public float Duration = 2f;
    public GameObject TitleLight;

    [Header("EndLevelScene")]
    public Material RobotUV;
    public Sprite[] levelSprites;
    public Image nextLevelImage;
    public Text scoreText;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        camSwitcher = Camera.main.GetComponent<CamSwitcher>();
        levelGen = GameObject.Find("LevelManager").GetComponent<LevelGen>();
        playerMovement = player.GetComponent<PlayerMovement>();
        

        TitleScreen.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "$"+gameManager.Score.ToString();
    }

    public void CamStateChange(int newCamera){
        camSwitcher.camState = newCamera;
    }


    public IEnumerator Countdown() {
        RandomiseThePlayer();
        SetTheNextLevelGraphic();
        //5
        CountDownBox.text = timer.ToString();
        timer--;
        yield return new WaitForSeconds (1f);
        //4
        CountDownBox.text = timer.ToString();
        timer--;
        yield return new WaitForSeconds (1f);
        //3
        CountDownBox.text = timer.ToString();
        timer--;
        yield return new WaitForSeconds (1f);
        //2
        CountDownBox.text = timer.ToString();
        timer--;
        yield return new WaitForSeconds (1f);
        //1
        CountDownBox.text = timer.ToString();
        timer--;
        yield return new WaitForSeconds (1f);
        //0
        CountDownBox.text = timer.ToString();
        timer--;
        yield return new WaitForSeconds (1f);
        titleMonitor.transform.DOMoveY(-5f, 2f);
        yield return new WaitForSeconds (1f);
        titleLogo.transform.DOScale(new Vector3(5f, 5f, 5f), 2).SetEase(Ease.InSine);
        camSwitcher.camState = 4;
        gameManager.GameMode = 4;
        StartCoroutine(FadeTheScreen());
    }

    public IEnumerator FadeTheScreen(){
        float counter = 0f;


        while(counter < Duration) {
            counter += Time.deltaTime;
            TitleScreen.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1f, 0, counter/Duration);

            yield return null;
        }

        TitleScreen.enabled = false;
    }


    public void RandomiseThePlayer() {
        float offset;
        offset = Random.Range (0,32) * 0.03f;
        Debug.Log (offset);
        RobotUV.mainTextureOffset =  new Vector2(offset, 0);
    }

    public void SetTheNextLevelGraphic() {
        nextLevelImage.sprite = levelSprites[levelGen.NextRoom];
    }

    public void StartNextLevel(){
        camSwitcher.camState = 0;
        gameManager.GameMode = 0;
        playerMovement.canMove = true;
        TitleLight.SetActive(false);
    }





}
