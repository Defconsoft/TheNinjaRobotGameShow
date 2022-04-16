using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public Transform SoundContainer;

    [Header("SFX Arrays")]
    public AudioClip[] audienceClapsOverlay;
    public AudioClip[] BulletSnd;//
    public AudioClip[] CrowdCheering;
    public AudioClip[] CrowdUpset;
    public AudioClip[] GameRotate;
    public AudioClip[] EnemyDeath;

    [Header("One Shots")]
    public AudioClip audienceStart;//
    public AudioClip ButtonHover;
    public AudioClip ButtonClick;
    public AudioClip JumpSnd;//
    public AudioClip DoorOpen;//
    public AudioClip DoorClose;//
    public AudioClip EnemySpawn;
    public AudioClip EnemyMove;
    public AudioClip PlayerHit;
    public AudioClip GameChoice;
    public AudioClip CoinSpawn;   
    public AudioClip CoinCollect;   
    public AudioClip BurnerSnd;//   
    public AudioClip FireSnd;//   
    public AudioClip FireHit;  
    public AudioClip PlatformDown;//  
    public AudioClip PlatformUp;//
    public AudioClip SpawnCoin;
    public AudioClip SpawnBox;
    public AudioClip BoxExplode;

    [Header("Loops")]
    public AudioClip audienceClaps;//
    public AudioClip ConveyorSnd;



    [Header("Music")]
    public AudioClip ThemeTune;//
    public AudioClip ImpendingJingle;//
    public AudioClip JustShootJingle;//
    public AudioClip GoldenGrabJingle;//
    public AudioClip ConveyorChaosJingle;//


    public AudioSource BGMusicAS;


    public void PlayStartAudience(){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = audienceStart;
        audioSource.PlayOneShot(audienceStart);
        Destroy(soundGameObject, audienceStart.length);
    }

    public void PlayLaserShot(Vector3 position){
        int soundChoice = Random.Range(0, BulletSnd.Length);
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = BulletSnd[soundChoice];
        audioSource.spatialBlend = 1f;
        audioSource.Play();
        Destroy(soundGameObject, BulletSnd[soundChoice].length);
    }

    public void PlayEnemyDeath(Vector3 position){
        int soundChoice = Random.Range(0, EnemyDeath.Length);
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = EnemyDeath[soundChoice];
        audioSource.spatialBlend = 1f;
        audioSource.Play();
        Destroy(soundGameObject, EnemyDeath[soundChoice].length);
    }

    public void PlayJumpSnd(Vector3 position){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = JumpSnd;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
        Destroy(soundGameObject, JumpSnd.length);
    }

    public void PlayDoorOpen(Vector3 position){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = DoorOpen;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
        Destroy(soundGameObject, DoorOpen.length);
    }

    public void PlayDoorClose(Vector3 position){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = DoorClose;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
        Destroy(soundGameObject, DoorClose.length);
    }

    public void PlayPlatformUp(Vector3 position){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = PlatformUp;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
        Destroy(soundGameObject, PlatformUp.length);
    }

    public void PlayPlatformDown(Vector3 position){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = PlatformDown;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
        Destroy(soundGameObject, PlatformDown.length);
    }

    public void PlayBurner(Vector3 position){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = BurnerSnd;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
        Destroy(soundGameObject, BurnerSnd.length);
    }

    public void PlayFlames(Vector3 position){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = FireSnd;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
        Destroy(soundGameObject, FireSnd.length);
    }



    /////////MUSIC//////////////////////
    public void PlayTheme(){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = ThemeTune;
        audioSource.PlayOneShot(ThemeTune);
        Destroy(soundGameObject, ThemeTune.length);
    }

    public void PlayJingleJustShoot(){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = JustShootJingle;
        audioSource.volume = 0.75f;
        audioSource.PlayOneShot(JustShootJingle);
        Destroy(soundGameObject, JustShootJingle.length);
    }

    public void PlayJingleGoldenGrab(){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GoldenGrabJingle;
        audioSource.volume = 0.75f;
        audioSource.PlayOneShot(GoldenGrabJingle);
        Destroy(soundGameObject, GoldenGrabJingle.length);
    }

    public void PlayJingleConveyorChaos(){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = ConveyorChaosJingle;
        audioSource.volume = 0.75f;
        audioSource.PlayOneShot(ConveyorChaosJingle);
        Destroy(soundGameObject, ConveyorChaosJingle.length);
    }

    public void PlayImpending(){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = ImpendingJingle;
        audioSource.volume = 0.75f;
        audioSource.PlayOneShot(ImpendingJingle);
        Destroy(soundGameObject, ImpendingJingle.length);
    }

    public void PlayClapLoops(){
        GameObject soundGameObject = new GameObject("sfx");
        soundGameObject.transform.parent = SoundContainer;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = audienceClaps;
        audioSource.volume = 0.1f;
        audioSource.loop = true;
        audioSource.Play();
    }


    public void PlayBGMusic(){
        BGMusicAS.Play();
        StartCoroutine(FadeInBG());
    }

    public void StopBGMusic(){
        StartCoroutine(FadeOutBG());
    }

    IEnumerator FadeInBG(){

        while(BGMusicAS.volume < 0.1f){
            BGMusicAS.volume = BGMusicAS.volume + 0.01f;
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator FadeOutBG(){

        while(BGMusicAS.volume > 0){
            BGMusicAS.volume = BGMusicAS.volume - 0.01f;
            yield return new WaitForEndOfFrame();
        }

        BGMusicAS.Stop();

    }


}
