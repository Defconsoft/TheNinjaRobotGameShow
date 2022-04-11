using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightMover : MonoBehaviour
{
    public Vector3[] points;
    // Start is called before the first frame update
    void Start()
    {
       
        var sequence = DOTween.Sequence();
        sequence.SetLoops(-1, LoopType.Yoyo);
        foreach (Vector3 point in points)
        {
            sequence.Append(transform.DOMove(point, (Random.Range(3f, 5f))).SetEase(Ease.InOutSine));
        } 
        
        GetComponent<Light>().DOColor(new Color(0,0,1), 5).SetLoops(-1, LoopType.Yoyo);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
