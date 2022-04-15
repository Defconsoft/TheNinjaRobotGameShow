using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public Transform SoundContainer;

    [Header("SFX Arrays")]
    public AudioClip[] audienceClaps;


    [Header("One Shots")]
    public AudioClip audienceStart;



    public void PlayStartAudience(){
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = audienceStart;
        audioSource.PlayOneShot(audienceStart);
        Destroy(soundGameObject, audienceStart.length);
    }



}
