using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("TopDown Variables")]
    [SerializeField] private float TopDownPlayerSpeed = 5f;
    [SerializeField] private float TopDownGravityValue = -30f;

    [Header("SideView Variables")]
    [SerializeField] private float SideViewPlayerSpeed = 5f;
    [SerializeField] private float SideViewJumpHeight = 1f;
    [SerializeField] private float SideViewGravityValue = -9.81f;

    [Header("Isometric Variables")]
    [SerializeField] private float IsometricPlayerSpeed = 5f;
    [SerializeField] private float IsometricGravityValue = -9.81f;   


    [SerializeField] private bool groundedPlayer;


    public bool canMove;
    public AudioSource moveSnd;
    public float moveVolume;
    private CharacterController controller;

    private Vector2 movement;
    private Vector2 aim;

    private Vector3 playerVelocity;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private Vector3 move;
    private GameManager gameManager;


    private void Awake() {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canMove = false;
    }


    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    void Update()
    {

        if (canMove){
            if (gameManager.GameMode == 0 || gameManager.GameMode == 3){
                HandleInput();
                IsometricHandleMovement();
                IsometricHandleRotation();

            } else if (gameManager.GameMode == 1) {
                HandleInput();
                TopDownHandleMovement();
                TopDownHandleRotation();

            } else if (gameManager.GameMode == 2) {
                HandleInput();
                SideViewHandleMovement();
            }
        }


        if (move != Vector3.zero && controller.isGrounded && canMove) {
            moveSnd.volume = moveVolume;
        } else {
            moveSnd.volume = 0;
        }

      
    }

    void HandleInput(){
        movement = playerControls.Controls.TopDownMovement.ReadValue<Vector2>();
        aim = playerControls.Controls.TopDownAim.ReadValue<Vector2>();
       
    }

    //////////////////////////////////
    //TOP DOWN MOVEMENT
    //////////////////////////////////


    void TopDownHandleMovement(){
        move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * TopDownPlayerSpeed);


        playerVelocity.y += TopDownGravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    void TopDownHandleRotation(){

        Ray ray = Camera.main.ScreenPointToRay(aim);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance)){
            Vector3 point = ray.GetPoint(rayDistance);
            LookAt(point);
        }

    }


    private void LookAt(Vector3 lookPoint){
        Vector3 heightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    //////////////////////////////////
    //SIDE VIEW MOVEMENT
    //////////////////////////////////


    private void SideViewHandleMovement()
    {

        if (playerControls.Controls.Jump.triggered && controller.isGrounded) {
            playerVelocity.y = Mathf.Sqrt(SideViewJumpHeight * -2f * SideViewGravityValue);
            GameObject.Find("SoundManager").GetComponent<SFXManager>().PlayJumpSnd(this.transform.position);
        }

        move = new Vector3(0, 0, movement.x);
        
        controller.Move(move * Time.deltaTime * SideViewPlayerSpeed);

        playerVelocity.y += SideViewGravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    //////////////////////////////////
    //ISOMETRIC VIEW MOVEMENT
    //////////////////////////////////

    private void IsometricHandleMovement()
    {
        /* if (playerControls.Controls.Jump.triggered && controller.isGrounded) {
            playerVelocity.y = Mathf.Sqrt(IsometricJumpHeight * -2f * IsometricGravityValue);
        } */

        move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * IsometricPlayerSpeed);


        playerVelocity.y += IsometricGravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void IsometricHandleRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(aim);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance)){
            Vector3 point = ray.GetPoint(rayDistance);
            LookAt(point);
        }
    }    


    public IEnumerator CamTransition() {
        canMove = false;
        yield return new WaitForSeconds (1f);
        canMove = true;
    }


}
