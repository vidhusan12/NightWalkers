using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //fixed movement speed
    public float moveSpeed;
    private Rigidbody myRigidbody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 playerPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        playerPosition = transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the player's input for movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //Calculate the players velocity based on the input and the speed
        moveInput = new Vector3(horizontalInput, 0f,verticalInput);
        moveVelocity = moveInput * moveSpeed;
        /*
        //set the player position for interpolation 
        playerPosition = transform.position + moveVelocity * Time.deltaTime;
    
        //Move the player using interpolation
         transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * moveSpeed);
         */
         transform.position += moveVelocity * Time.deltaTime;
    }
}
