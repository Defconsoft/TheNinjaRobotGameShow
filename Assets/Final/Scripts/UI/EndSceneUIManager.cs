using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndSceneUIManager : MonoBehaviour
{
    private Canvas thisEndScreen;
    private Camera UIcamera;
    public Button StartBtn;
    bool changingLevel;
    public Image nextLevelImage;
    private GameManager gameManager;
    public Text scoreText;
    public Sprite[] levelSprites;
    public LevelGen levelGen;
    // Start is called before the first frame update
    void Start()
    {
        UIcamera = Camera.main;
        levelGen = GameObject.Find("LevelManager").GetComponent<LevelGen>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartBtn.onClick.RemoveAllListeners();
        StartBtn.onClick.AddListener(() => GameObject.Find("UIManager").GetComponent<UIManager>().StartNextLevel());
        StartCoroutine(CycleSprites());
    }

    public void FindCamera(){
        thisEndScreen.GetComponent<Canvas>().worldCamera = UIcamera;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "$"+gameManager.Score.ToString();
    }

    public void SetTheNextLevelGraphic() {
        changingLevel = true;
        nextLevelImage.sprite = levelSprites[levelGen.NextRoom];
        GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayGameSelect(this.transform.position);
        nextLevelImage.GetComponent<RectTransform>().DOPunchScale(new Vector3(1f, 1f, 1f), 1f, 5, 1f);
    }


    IEnumerator CycleSprites(){
        int index = 0;

        while (!changingLevel){
            if (index >= levelSprites.Length - 1){
                index = 0;
            } else {
                index += 1;
            }
            nextLevelImage.sprite = levelSprites[index];
            yield return new WaitForSeconds(0.3f);
            GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayGameRotate(this.transform.position);
        }

        yield return null;
    }




}
