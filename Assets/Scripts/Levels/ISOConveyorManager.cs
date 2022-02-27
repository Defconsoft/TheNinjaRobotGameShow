using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOConveyorManager : MonoBehaviour
{

    [Header("FallAway")]
    public GameObject FallAway;
    public float lerpTime;
    float currentLerpTime;
    private float perc = 0f;
    public float moveDistance = 1f;
    bool removeFallAway = false;
    Vector3 startPos;
    Vector3 endPos;

    [Header("ConveyorStuff")]
    public GameObject conBelt;
    private ISOConveyorBelt conBeltComp;


    private void Start() {
        startPos = FallAway.transform.position;
        endPos = FallAway.transform.position - transform.up * moveDistance;
        conBelt = transform.Find("Platform").gameObject;
        conBeltComp = conBelt.GetComponent<ISOConveyorBelt>();
        
    }


    public void StartISOGame() {
        removeFallAway = true;
        conBeltComp.active = true;
    }


    public void Update() {

        if (removeFallAway) {
            RemoveFallAway();
        }

        if (perc >= 1.0) {
            removeFallAway = false;
            Destroy(FallAway, 0.1f);
        }
        

    }
    
    void RemoveFallAway(){
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime) {
            currentLerpTime = lerpTime;
        }
 
        //lerp!
        perc = currentLerpTime / lerpTime;
        FallAway.transform.position = Vector3.Lerp(startPos, endPos, perc);
    }

}
