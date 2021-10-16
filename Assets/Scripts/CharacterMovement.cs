using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Camera cam;
    public float speed = 5f;
    CharacterController characterController;

    float mDesiredRotation = 0f;
    float RotationSpeed = 15f;

    float mSpeedY = 0;
    float mGravity = -9.81f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if(!characterController.isGrounded)
        {
            mSpeedY += mGravity * Time.deltaTime;
        }
        else
        {
            mSpeedY = 0;
        }

        mSpeedY += mGravity * Time.deltaTime;
        Vector3 movement = new Vector3(x, 0, z).normalized;
        Vector3 rotatedMovement = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * movement;
        Vector3 verticalMovement = Vector3.up * mSpeedY;
        characterController.Move(verticalMovement + (rotatedMovement * speed) * Time.deltaTime);

        if (rotatedMovement.magnitude > 0)
        {
            mDesiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            
        }
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

}
