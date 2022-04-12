using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;


public class CamSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera Traversal;
    public CinemachineVirtualCamera TopDown;
    public CinemachineVirtualCamera SideView;
    public CinemachineVirtualCamera Isometric;
    public CinemachineVirtualCamera UICam;
    public CinemachineVirtualCamera TitleCam;
    public CinemachineVirtualCamera DeathCam;

    public CinemachineBrain mainBrain;

    public int camState;

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
        mainBrain = GetComponent<CinemachineBrain>();
        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        m_camera = GetComponent<Camera>();
        m_camera.projectionMatrix = perspective;
        blender = (MatrixBlender)GetComponent(typeof(MatrixBlender));

    }

    // Update is called once per frame
    void Update()
    {
        switch (camState) {
            case 0: //Traversal
                mainBrain.m_DefaultBlend.m_Time = 1f;
                Traversal.m_Priority = 10;
                TopDown.m_Priority = 1;
                SideView.m_Priority = 1;
                Isometric.m_Priority = 1;
                UICam.m_Priority = 1;
                TitleCam.m_Priority = 1;
                DeathCam.m_Priority = 1;

            break;

            case 1: // TopDown
                mainBrain.m_DefaultBlend.m_Time = 1f;
                Traversal.m_Priority = 1;
                TopDown.m_Priority = 10;
                SideView.m_Priority = 1;
                Isometric.m_Priority = 1;
                UICam.m_Priority = 1;
                TitleCam.m_Priority = 1;
                DeathCam.m_Priority = 1;
            break;

            case 2: // Side
                mainBrain.m_DefaultBlend.m_Time = 1f;
                Traversal.m_Priority = 1;
                TopDown.m_Priority = 1;
                SideView.m_Priority = 10;
                Isometric.m_Priority = 1;
                UICam.m_Priority = 1;
                TitleCam.m_Priority = 1;

            break;

            case 3: // Isomteric
                mainBrain.m_DefaultBlend.m_Time = 1f;
                Traversal.m_Priority = 1;
                TopDown.m_Priority = 1;
                SideView.m_Priority = 1;
                Isometric.m_Priority = 10;
                UICam.m_Priority = 1;
                TitleCam.m_Priority = 1;
                DeathCam.m_Priority = 1;
            break;

            case 4: // UI
                mainBrain.m_DefaultBlend.m_Time = 2f;
                Traversal.m_Priority = 1;
                TopDown.m_Priority = 1;
                SideView.m_Priority = 1;
                Isometric.m_Priority = 1;
                UICam.m_Priority = 10;
                TitleCam.m_Priority = 1;
                DeathCam.m_Priority = 1;
            break;

            case 5: //TitleCam
                mainBrain.m_DefaultBlend.m_Time = 2f;
                Traversal.m_Priority = 1;
                TopDown.m_Priority = 1;
                SideView.m_Priority = 1;
                Isometric.m_Priority = 1;
                UICam.m_Priority = 1;
                TitleCam.m_Priority = 10;
                DeathCam.m_Priority = 1;
            break;

            case 6: //DeathCam
                mainBrain.m_DefaultBlend.m_Time = 1f;
                Traversal.m_Priority = 1;
                TopDown.m_Priority = 1;
                SideView.m_Priority = 1;
                Isometric.m_Priority = 1;
                UICam.m_Priority = 1;
                TitleCam.m_Priority = 1;
                DeathCam.m_Priority = 10;
            break;
        }

    }


    public void BlendToPerspective(){
        blender.BlendToMatrix(perspective, 1f, 8,false);
    }

    public void BlendToOrtho(){
        blender.BlendToMatrix(ortho, 1f, 8, true);
    }

}
