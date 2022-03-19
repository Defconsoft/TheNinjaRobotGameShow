using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [Header ("Game States")] 
    public int GameMode; //0 - Traversal, 1 - TopDownShooter, 2 - Side On, 3 - Isometric
    private GameObject mainCamera;
    private CamSwitcher camSwitcher;



    private void Start() {
        camSwitcher = Camera.main.gameObject.GetComponent<CamSwitcher>();
    }

    private void Update() {

        //camSwitcher.camState = GameMode;

        //player death from falling
        if (transform.position.y <= -4f) {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }

    }






}
