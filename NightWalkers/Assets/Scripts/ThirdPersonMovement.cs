using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    //Input fields
    private PlayerControls playerActionAssest;
    private InputAction move;

    private new Rigidbody rigidbody;
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jusmpForce = 5f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;

    
    private void Awake()
    {
   
        rigidbody = this.GetComponent<Rigidbody>();
        playerActionAssest = new PlayerControls();
    }

    private void OnEnable()
    {
        playerActionAssest.Player.Move.started += DoJump;
        move = playerActionAssest.Player.Move;
        playerActionAssest.Player.Enable();
    }

   

    private void OnDisable()
    {
        playerActionAssest.Player.Jump.started -= DoJump;
        playerActionAssest.Player.Disable();
    }

    private void FixedUpdate()
    {
        //Calcukatubg the force direction for player movement based on camera direction and input value
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        //applying the force to the player rigibody
        rigidbody.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if(rigidbody.velocity.y < 0f)
            rigidbody.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        //capping the speed the player doesnt run so fast
        Vector3 horVelocity = rigidbody.velocity;
        horVelocity.y = 0;
        if (horVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rigidbody.velocity = horVelocity.normalized * maxSpeed + Vector3.up * rigidbody.velocity.y;

        LookAt();

    }

    private void LookAt()
    {
        Vector3 direction = rigidbody.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rigidbody.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rigidbody.angularVelocity = Vector3.zero;
    }

    //Getting the right direction of the camera
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
        
    }

    //Getting the forward direction of the camera 
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    //Doing the jump action
    private void DoJump(InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jusmpForce;
        }
        
    }
    //Checking if the player is grounded
    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false;
    }

}
