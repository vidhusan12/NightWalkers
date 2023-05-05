using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotationSpeed = 500f;
    PlayerCamerController cameraController;
    Quaternion playerRotation;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<PlayerCamerController>();
        
    }

    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(hor) + Mathf.Abs(ver);

        var moveInput = (new Vector3(hor, 0, ver)).normalized;
        var moveDir = cameraController.planarRotation * moveInput;

        if(moveAmount > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            //setting the rotating of the character of the move so that it will face the direction of the character moving 
            playerRotation = Quaternion.LookRotation(moveDir);
        }
        //rotates the player smoothly instead of instanely 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);

    }

}
