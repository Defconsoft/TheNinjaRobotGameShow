using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorTrigger : MonoBehaviour
{

    public bool open, close;

    public GameObject door;

    public bool PlaySound;

    public float moveDistance = 1f;

    Vector3 endPos;
    public float lerpTime;
    float currentLerpTime;



    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Player") {
            if (open) {
                endPos = door.transform.position + door.transform.right * moveDistance;
                GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayDoorOpen(door.transform.position);
                StartCoroutine(LerpPosition(endPos, lerpTime));

            }

            if (close) {
                endPos = door.transform.position - door.transform.right * moveDistance;
                StartCoroutine(LerpPosition(endPos, lerpTime));
                GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayDoorClose(door.transform.position);                
                if (PlaySound){
                    GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayMovingOn();
                }

            }

            transform.position = new Vector3 (transform.position.x, transform.position.y - 20f, transform.position.z);
           
            Destroy (this.gameObject, 5f);
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration) {
        
        float time = 0;
        Vector3 startPosition = door.transform.position;
        while (time < duration)
        {
            door.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;

        }
        door.transform.position = targetPosition;
    }

}
