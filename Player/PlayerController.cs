using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float lookSpeed = 5f;


    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;    
    }

    private void Update() 
    {
        // Move
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Look
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        transform.Rotate(Vector3.up * mouseX);
        CinemachineVirtualCamera camera = GetComponentInChildren<CinemachineVirtualCamera>();
        camera.transform.Rotate(Vector3.left * mouseY);    
    }
}
