using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamerController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float rotationSpeed = 2;

    [SerializeField] float distance = 5;


    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;

    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    float invertXVal;
    float invertYVal;

    float rotationX;
    float rotationY;

    private void Start()
    {
        //Hiding the cursor from the game window and locking it 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y") *invertYVal * rotationSpeed; //Vertical rotation
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);


        rotationY += Input.GetAxis("Mouse X") *invertXVal * rotationSpeed; //Horzonal rotation

        //roating the Vecotr
        var PlayerRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var focusPostion = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        //Camera behind the player
        transform.position = focusPostion - PlayerRotation * new Vector3(0, 0, distance);
        transform.rotation = PlayerRotation;
    }
    //so the player cant move verticaly
    public Quaternion planarRotation => Quaternion.Euler(0, rotationY, 0);
}
