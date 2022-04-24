using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMove : MonoBehaviour
{
    
    // Scroll main texture based on time

    public float scrollSpeed = 0.5f;
    Material conveyMat;
    float offset;

    void Start()
    {
        conveyMat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        offset = offset - scrollSpeed;
        conveyMat.SetTextureOffset("_BaseMap", new Vector2(offset, 0));
    }
}
