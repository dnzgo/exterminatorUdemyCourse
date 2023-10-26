using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Joystick moveJoystick;
    [SerializeField] Joystick aimJoystick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float turnSpeed = 30f;
    [SerializeField] float animTurnSpeed = 5f;

    [Header("Inventory")]
    [SerializeField] InventoryComponent inventoryComponent;

    Vector2 moveInput;
    Vector2 aimInput;

    Camera mainCamera;
    CameraController cameraController;
    Animator animator;

    float animatorTurnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveJoystick.onStickValueUpdated += MoveInputUpdated;
        aimJoystick.onStickValueUpdated += AimInputUpdated;
        aimJoystick.onStickTaped += StartSwitchWeapon;
        mainCamera = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();
    }

    public void AttackPoint()
    {
        inventoryComponent.GetActiveWeapon().Attack();
    }

    void StartSwitchWeapon()
    {
        animator.SetTrigger("switchWeapon");
    }

    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }

    void AimInputUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
        if (aimInput.magnitude > 0)
        {
            animator.SetBool("attacking", true);
        }
        else
        {
            animator.SetBool("attacking", false);
        }
    }
    void MoveInputUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    Vector3 StickInputToWorldDir(Vector2 inputValue)
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * inputValue.x + upDir * inputValue.y;
    }

    // Update is called once per frame
    void Update()
    {
        PerformMoveAndAim();
        UpdateCamera();

    }

    private void PerformMoveAndAim()
    {
        Vector3 moveDir = StickInputToWorldDir(moveInput);
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);
        UpdateAim(moveDir);

        float forward = Vector3.Dot(moveDir, transform.forward);
        float right = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);

    }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 aimDir = currentMoveDir;
        if (aimInput.magnitude != 0)
        {
            aimDir = StickInputToWorldDir(aimInput);
        }

        RotateTowards(aimDir);
    }

    private void UpdateCamera()
    {
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = 0f;
        if (aimDir.magnitude != 0)
        {
            Quaternion previousRotation = transform.rotation;

            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerpAlpha);

            Quaternion currentRotation = transform.rotation;
            float direction = Vector3.Dot(aimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(previousRotation, currentRotation) * direction;
            currentTurnSpeed = rotationDelta / Time.deltaTime;

        }
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);
        animator.SetFloat("turningSpeed", animatorTurnSpeed);
    }
}
