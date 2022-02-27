using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{


    [Header("Debug")]
    public bool canMove;
    private Rigidbody rb;


    [Header("TopDown Variables")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSpeed = 5f;

    [SerializeField] private float TopDownPlayerSpeed = 5f;
    [SerializeField] private float TopDownGravityValue = -30f;

    [Header("SideView Variables")]
    [SerializeField] private float SideViewPlayerSpeed = 5f;
    [SerializeField] private float SideViewJumpForce = 1f;
    [SerializeField] private float SideViewGravityValue = -9.81f;

    [Header("Isometric Variables")]
    [SerializeField] private float IsometricPlayerSpeed = 5f;
    [SerializeField] private float IsometricJumpForce = 1f;
    [SerializeField] private float IsometricGravityValue = -9.81f;   



    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector2 aim;

    private CamSwitcher camSwitcher;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        camSwitcher = Camera.main.GetComponent<CamSwitcher>();
    }


    private void Update() {
        moveInput = new Vector3 (Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;
    }







    public IEnumerator CamTransition() {
        canMove = false;
        yield return new WaitForSeconds (1f);
        canMove = true;
    }


}
