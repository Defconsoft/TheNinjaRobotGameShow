using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartGameUI : MonoBehaviour
{

    public Image StartImage;
    public enum GameModes {
        Shoot,
        Grab,
        Convey
    }
    public GameModes gameModes;
    // Start is called before the first frame update
    void Start()
    {
        var sequence1 = DOTween.Sequence();

        sequence1.Append(StartImage.GetComponent<RectTransform>().DOScale(new Vector3 (1, 1, 1), 1.5f).SetEase(Ease.OutBounce));
        sequence1.AppendInterval(1f);
        sequence1.Append(StartImage.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (0, -968f), 1.5f).SetEase(Ease.InQuart)); 

        if (gameModes == GameModes.Shoot){
            GameObject.Find("SoundManager").GetComponent<SpeechManager>().shootIntro();
        } else if (gameModes == GameModes.Grab) {
            GameObject.Find("SoundManager").GetComponent<SpeechManager>().goldGrabIntro();
        } else if (gameModes == GameModes.Convey) {
            GameObject.Find("SoundManager").GetComponent<SpeechManager>().conveyorIntro();
        }
    }


}
