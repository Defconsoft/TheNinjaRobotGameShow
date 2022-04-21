using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Loading : MonoBehaviour
{

    public string sceneToLoad;
    AsyncOperation loadingOperation;
    public Slider progressBar;

    private void Start() {
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);

    }


    private void Update() {
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }


}
