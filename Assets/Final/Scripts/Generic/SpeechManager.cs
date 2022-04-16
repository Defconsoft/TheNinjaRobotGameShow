using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    public Transform SoundContainer;

    [Header("Sound Arrays")]
    public AudioClip[] bombHit;//
    public AudioClip[] coinCollect;//
    public AudioClip[] contestantMeet;
    public AudioClip[] episodeStart;//
    public AudioClip[] fireDamage;
    public AudioClip[] gameLoss;//
    public AudioClip[] movingOn;//
    public AudioClip[] nextGame;//
    public AudioClip[] progress;//
    public AudioClip[] shootTakeDamage;//
    public AudioClip[] shootDoDamage;//
    public AudioClip[] gameWin;//
        
    [Header("One Shots")]
    public AudioClip intro;//
    public AudioClip goldenGrabIntro;//
    public AudioClip justShootIntro;//
    public AudioClip conveyorChaosIntro;//


    private bool IsoCollect;
    private float IsoCollectTime;

    private bool ShootBullet;
    private float ShootTimer;

    public void PlayShootSpeech(bool isBullet){

        if (!ShootBullet){
            ShootBullet = true;
            float tempfloat = Random.Range(0,1f);

            if (!isBullet){
                if (tempfloat >= 0.5f){
                    int soundChoice = Random.Range(0, shootTakeDamage.Length);
                    GameObject soundGameObject = new GameObject("speech");
                    soundGameObject.transform.parent = SoundContainer;
                    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                    audioSource.clip = shootTakeDamage[soundChoice];
                    audioSource.PlayOneShot(shootTakeDamage[soundChoice]);
                    ShootTimer = shootTakeDamage[soundChoice].length;
                    Destroy(soundGameObject, shootTakeDamage[soundChoice].length);
                }
            } else {
                if (tempfloat >= 0.5f){
                    int soundChoice = Random.Range(0, shootDoDamage.Length);
                    GameObject soundGameObject = new GameObject("speech");
                    soundGameObject.transform.parent = SoundContainer;
                    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                    audioSource.clip = shootDoDamage[soundChoice];
                    audioSource.PlayOneShot(shootDoDamage[soundChoice]);
                    ShootTimer = shootDoDamage[soundChoice].length;
                    Destroy(soundGameObject, shootDoDamage[soundChoice].length);
                }
            }
            StartCoroutine(StartShootTimer());
        }
    }

    IEnumerator StartShootTimer(){
        yield return new WaitForSeconds(ShootTimer);
        ShootBullet = false;
    }


    public void PlayIsoCollect(bool isBomb){

        if (!IsoCollect){
            IsoCollect = true;
            float tempfloat = Random.Range(0,1f);

            if (!isBomb){
                if (tempfloat >= 0.5f){
                    int soundChoice = Random.Range(0, coinCollect.Length);
                    GameObject soundGameObject = new GameObject("speech");
                    soundGameObject.transform.parent = SoundContainer;
                    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                    audioSource.clip = coinCollect[soundChoice];
                    audioSource.PlayOneShot(coinCollect[soundChoice]);
                    IsoCollectTime = coinCollect[soundChoice].length;
                    Destroy(soundGameObject, coinCollect[soundChoice].length);
                }
            } else {
                if (tempfloat >= 0.5f){
                    int soundChoice = Random.Range(0, bombHit.Length);
                    GameObject soundGameObject = new GameObject("speech");
                    soundGameObject.transform.parent = SoundContainer;
                    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                    audioSource.clip = bombHit[soundChoice];
                    audioSource.PlayOneShot(bombHit[soundChoice]);
                    IsoCollectTime = bombHit[soundChoice].length;
                    Destroy(soundGameObject, bombHit[soundChoice].length);
                }
            }
            StartCoroutine(StartIsoCollectTimer());
        }
    }

    IEnumerator StartIsoCollectTimer(){
        yield return new WaitForSeconds(IsoCollectTime);
        IsoCollect = false;
    }

    public void PlayYouLose(){
        int soundChoice = Random.Range(0, gameLoss.Length);
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = gameLoss[soundChoice];
        audioSource.PlayOneShot(gameLoss[soundChoice]);
        Destroy(soundGameObject, gameLoss[soundChoice].length);
    }

    public void PlayNextGame(){
        int soundChoice = Random.Range(0, nextGame.Length);
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = nextGame[soundChoice];
        audioSource.PlayOneShot(nextGame[soundChoice]);
        Destroy(soundGameObject, nextGame[soundChoice].length);
    }

    public void PlayMovingOn(){
        int soundChoice = Random.Range(0, movingOn.Length);
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = movingOn[soundChoice];
        audioSource.PlayOneShot(movingOn[soundChoice]);
        Destroy(soundGameObject, movingOn[soundChoice].length);
    }

    public void PlayProgress(){
        int soundChoice = Random.Range(0, progress.Length);
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = progress[soundChoice];
        audioSource.PlayOneShot(progress[soundChoice]);
        Destroy(soundGameObject, progress[soundChoice].length);
    }


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

    public void PlayNextContestant(){
        int soundChoice = Random.Range(0, contestantMeet.Length);
        GameObject soundGameObject = new GameObject("speech");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = contestantMeet[soundChoice];
        audioSource.PlayOneShot(contestantMeet[soundChoice]);
        Destroy(soundGameObject, contestantMeet[soundChoice].length);
    }




}
