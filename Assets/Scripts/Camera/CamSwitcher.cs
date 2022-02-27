using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;


public class CamSwitcher : MonoBehaviour
{

    public CinemachineVirtualCamera TopDown;
    public CinemachineVirtualCamera SideView;
    public CinemachineVirtualCamera Isometric;


    public int camState = 2;

    private Matrix4x4 ortho,
                        perspective;
    public float fov = 60f, near = .3f, far = 1000f, orthographicSize = 7f;
    private float aspect;
    private MatrixBlender blender;
    private bool orthoOn;
    Camera m_camera;




    // Start is called before the first frame update
    void Start()
    {
        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        m_camera = GetComponent<Camera>();
        m_camera.projectionMatrix = ortho;
        blender = (MatrixBlender)GetComponent(typeof(MatrixBlender));

    }

    // Update is called once per frame
    void Update()
    {
        
        switch (camState) {
            case 0: //Top Down
                TopDown.m_Priority = 10;
                SideView.m_Priority = 1;
                Isometric.m_Priority = 1;
            break;

            case 1: // Side View
                TopDown.m_Priority = 1;
                SideView.m_Priority = 10;
                Isometric.m_Priority = 1;
            break;

            case 2: // Isometric
                TopDown.m_Priority = 1;
                SideView.m_Priority = 1;
                Isometric.m_Priority = 10;
            break;

        }

    }


    public void BlendToPerspective(){
        blender.BlendToMatrix(perspective, 1f, 8,false);
    }

    public void BlendToOrtho(){
        blender.BlendToMatrix(ortho, 1f, 8,true);
    }

}
