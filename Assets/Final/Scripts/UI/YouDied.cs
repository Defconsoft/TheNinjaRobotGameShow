using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class YouDied : MonoBehaviour
{
    public Image You;
    public Image Died;

    // Start is called before the first frame update
    void Start()
    {

        var sequence1 = DOTween.Sequence();

        sequence1.Append(You.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (-123f, 112.5f), 1.5f).SetEase(Ease.OutQuart));
        sequence1.AppendInterval(0.3f);
        sequence1.Append(You.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (1218.5f, 112.5f), 1.5f).SetEase(Ease.InQuart));



        var sequence2 = DOTween.Sequence();
        sequence2.AppendInterval(0.3f);
        sequence2.Append(Died.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (194.25f, -105f), 1.5f).SetEase(Ease.OutQuart));
        sequence2.AppendInterval(0.3f);
        sequence2.Append(Died.GetComponent<RectTransform>().DOAnchorPos(new Vector2 (1508.5f, -105f), 1.5f).SetEase(Ease.InQuart));

        GameObject.Find("SoundManager").GetComponent<SpeechManager>().PlayYouLose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
