using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDE_MovingPlatform : MonoBehaviour
{

    private Vector3 startPosition;
    private Vector3 nextPosition;
    private Vector3 currentTarget;




    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        nextPosition = new Vector3 (startPosition.x, startPosition.y + Random.Range (1f, 4f), startPosition.z);
        currentTarget = nextPosition;
        transform.position = currentTarget;
    }

}
