using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    public Transform SoundContainer;

    [Header("Sound Arrays")]
    public AudioClip[] bombHit;
    public AudioClip[] coinCollect;
    public AudioClip[] contestantMeet;
    public AudioClip[] episodeStart;
    public AudioClip[] fireDamage;
    public AudioClip[] gameLoss;
    public AudioClip[] movingOn;
    public AudioClip[] nextGame;
    public AudioClip[] progress;
    public AudioClip[] shootTakeDamage;
    public AudioClip[] shootDoDamage;
    public AudioClip[] gameWin;
        
    [Header("One Shots")]
    public AudioClip intro;
    public AudioClip goldenGrabIntro;
    public AudioClip justShootIntro;
    public AudioClip conveyorChaosIntro;

    public void YouWin(){
        int soundChoice = Random.Range(0, gameWin.Length);
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = gameWin[soundChoice];
        audioSource.PlayOneShot(gameWin[soundChoice]);
        Destroy(soundGameObject, gameWin[soundChoice].length);
    }

    public void goldGrabIntro(){
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = goldenGrabIntro;
        audioSource.PlayOneShot(goldenGrabIntro);
        Destroy(soundGameObject, goldenGrabIntro.length);
    }

    public void shootIntro(){
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = justShootIntro;
        audioSource.PlayOneShot(justShootIntro);
        Destroy(soundGameObject, justShootIntro.length);
    }

    public void conveyorIntro(){
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = conveyorChaosIntro;
        audioSource.PlayOneShot(conveyorChaosIntro);
        Destroy(soundGameObject, conveyorChaosIntro.length);
    }

    public void EpisodeStartSpeech1(){
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = episodeStart[0];
        audioSource.PlayOneShot(episodeStart[0]);
        Destroy(soundGameObject, episodeStart[0].length);
    }

    public void EpisodeStartSpeech2(){
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = episodeStart[1];
        audioSource.PlayOneShot(episodeStart[1]);
        Destroy(soundGameObject, episodeStart[1].length);
    }

    public void EpisodeStartSpeech3(){
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = episodeStart[2];
        audioSource.PlayOneShot(episodeStart[2]);
        Destroy(soundGameObject, episodeStart[2].length);
    }

    public void Welcome(){
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = intro;
        audioSource.PlayOneShot(intro);
        Destroy(soundGameObject, intro.length);
    }




}
