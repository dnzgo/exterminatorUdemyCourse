using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float turnSpeed = 30f;
    Vector2 moveInput;

    Camera mainCamera;
    CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        joystick.onStickValueUpdated += MoveInputUpdated;
        mainCamera = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
    }

    void MoveInputUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        Vector3 moveDir = rightDir * moveInput.x + upDir * moveInput.y;

        characterController.Move(moveDir * Time.deltaTime * moveSpeed);

        if (moveInput.magnitude != 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir, Vector3.up), turnLerpAlpha);
            if (cameraController != null)
            {
                cameraController.AddYawInput(moveInput.x);
            }
        }

    }
}
