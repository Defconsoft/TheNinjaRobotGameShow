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

    [Header("Videos")]
    public GameObject video1;
    public GameObject video2;
    public GameObject video3;
    public GameObject video4;

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
        yield return new WaitForSeconds (2f);
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayTheme();
        video1.SetActive(true);
        Sequence videoSequence1 = DOTween.Sequence();
        videoSequence1.Append(video1.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 3.5f));
        videoSequence1.Join(video1.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (-778f, 500), 3.5f));
        StartCoroutine(FadeVideo(video1));
        Destroy(video1, 10f);
        yield return new WaitForSeconds (1f);
        video2.SetActive(true);
        Sequence videoSequence2 = DOTween.Sequence();
        videoSequence2.Append(video2.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 3.5f));
        videoSequence2.Join(video2.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (778f, -500), 3.5f));
        StartCoroutine(FadeVideo(video2));
        Destroy(video2, 10f);
        yield return new WaitForSeconds (1f);
        video3.SetActive(true);
        Sequence videoSequence3 = DOTween.Sequence();
        videoSequence3.Append(video3.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 3.5f));
        videoSequence3.Join(video3.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (-778f, -500), 3.5f));
        StartCoroutine(FadeVideo(video3));
        Destroy(video3, 10f);
        yield return new WaitForSeconds (1f);
        video4.SetActive(true);
        Sequence videoSequence4 = DOTween.Sequence();
        videoSequence4.Append(video4.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 3.5f));
        videoSequence4.Join(video4.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (778f, 500), 3.5f));
        StartCoroutine(FadeVideo(video4));
        Destroy(video4, 10f);
        yield return new WaitForSeconds (1f);
        titleLogo.transform.DOScale(new Vector3(1f, 1f, 1f), 2).SetEase(Ease.InSine);

        GameObject.Find("SoundManager").GetComponent<SpeechManager>().Welcome();
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayClapLoops();
        yield return new WaitForSeconds (4f);
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayNextContestant();
        titleLogo.transform.DOScale(new Vector3(5f, 5f, 5f), 2).SetEase(Ease.InSine);
        camSwitcher.camState = 4;
        gameManager.GameMode = 4;
        StartCoroutine(FadeTheScreen());
    }

    public void StartGameAgain(){
        GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayNextContestant();
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayClapLoops();
        camSwitcher.camState = 4;
        gameManager.GameMode = 4;
    }

    public IEnumerator FadeVideo(GameObject tmp){
        yield return new WaitForSeconds(1.5f);
        float counter = 0f;
        float length = 2f;

            while(counter < length) {
            counter += Time.deltaTime;
            tmp.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1f, 0, counter/length);

            yield return null;
        }
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
