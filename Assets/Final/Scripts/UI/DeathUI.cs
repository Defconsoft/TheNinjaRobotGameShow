using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    public Button StartBtn;
    public Text RoomTxt;
    public Text ScoreTxt;
    private LevelGen levelGen;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        levelGen = GameObject.Find("LevelManager").GetComponent<LevelGen>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RoomTxt.text = "YOU COMPLETED " + (levelGen.roomNumber -1).ToString() + " ROOMS";
        ScoreTxt.text = "$"+gameManager.Score.ToString();
    }

    public void RestartGame(){
        DOTween.KillAll();
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

}
