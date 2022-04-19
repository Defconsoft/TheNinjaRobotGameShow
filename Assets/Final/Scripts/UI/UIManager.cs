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

    [Header("FirstSceneUI")] 
    public Canvas firstScene; 
    public Button firstStart;

    [Header("EndLevelScene")]
    public Material RobotUV;
    public Sprite[] levelSprites;
    public Image nextLevelImage;
    public Text scoreText;
    public GameObject currentEnd;

    [Header("InGameScreen")]
    public Canvas inGame;
    public GameObject InGamePanel;
    public int downPosition;
    public int upPosition;

    [Header("InGameScreen")]
    public Canvas quitScreen;
    public GameObject quitPanel;
    public GameObject fadeObject;
    public GameObject Quitbtn;

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
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().EpisodeStartSpeech2();
        CountDownBox.text = timer.ToString();
        timer--;
        yield return new WaitForSeconds (1f);
        //3
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().EpisodeStartSpeech3();
        CountDownBox.text = timer.ToString();
        timer--;
        yield return new WaitForSeconds (1f);
        //2
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayStartAudience();
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
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayTheme();
        yield return new WaitForSeconds (6f);
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().Welcome();
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayClapLoops();
        yield return new WaitForSeconds (2f);
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
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayNextContestant();

    }


    public void RandomiseThePlayer() {
        float offset;
        offset = Random.Range (0,32) * 0.03f;
        RobotUV.mainTextureOffset =  new Vector2(offset, 0);
    }

    public void SetTheNextLevelGraphic() {
        nextLevelImage.sprite = levelSprites[levelGen.NextRoom];
    }

    public void StartFirstLevel(){
        camSwitcher.camState = 0;
        gameManager.GameMode = 0;
        playerMovement.canMove = true;
        firstStart.interactable = false;
        //InGameMoveIn();
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayNextGame();     
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayImpending();
        TitleLight.SetActive(false);
    }

    public void InGameMoveIn() {
        InGamePanel.transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (1000, 1042), 1f);
    }

    public void InGameMoveOut() {
        InGamePanel.transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (1000, 1139), 1f);
    }

    public void EndLevelBegin(Transform movePoint, GameObject currentScene){
        camSwitcher.camState = 4;
        gameManager.GameMode = 4;
        //InGameMoveOut();
        currentEnd = currentScene;
        
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayProgress();
        StartCoroutine (EndLevelWait());
        player.GetComponent<PlayerMovement>().canMove = false;
        player.transform.DOMove(movePoint.position, 2);
    }

    IEnumerator EndLevelWait(){
        yield return new WaitForSeconds(3f);
        currentEnd.GetComponent<EndSceneUIManager>().SetTheNextLevelGraphic();
    }

    public void StartNextLevel(){
        camSwitcher.camState = 0;
        gameManager.GameMode = 0;
        playerMovement.canMove = true;
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayImpending();
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayNextGame();
        InGameMoveIn();
        currentEnd.GetComponent<EndSceneUIManager>().StartBtn.interactable = false;
    }

    public void QuitBtn(){
        Quitbtn.SetActive(false);
        fadeObject.SetActive (true);
        playerMovement.canMove = false;
        quitPanel.transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, 0), 1f).OnComplete(ChangeTime);
    }

    void ChangeTime(){
        Debug.Log("here");
        Time.timeScale = 0;   
    }

    public void QuitYes(){
        Application.Quit();
    }

    public void QuitNo(){
        Time.timeScale = 1;
        playerMovement.canMove = true;
        Quitbtn.SetActive(true);
        fadeObject.SetActive (false);
        quitPanel.transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (-1379, 0), 1f);
    }




}
