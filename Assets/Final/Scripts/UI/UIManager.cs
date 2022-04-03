using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{




    private CamSwitcher camSwitcher;
    private GameManager gameManager;


    [Header("TitleScreen")]
    public Canvas TitleScreen;
    public Text CountDownBox;
    private int timer = 5;
    public float Duration = 2f;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        camSwitcher = Camera.main.GetComponent<CamSwitcher>();

        TitleScreen.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CamStateChange(int newCamera){
        camSwitcher.camState = newCamera;
    }


    public IEnumerator Countdown() {
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
    }





}
