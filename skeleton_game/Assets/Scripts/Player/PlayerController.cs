// Written By: Damien Carlson
// Created On: 03/16/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Variables
    [SerializeField] private GameManager gameManager;
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public bool canMove = true;

    public float rotationSpeed = 2f;
    public float rotationXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    CharacterController characterController;
    

    // Methods
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canMove = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canMove = false;
    }

    void Update()
    {
        // Player Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

            // Sprinting
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Camera Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
            rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When player touches a gem, destroy the gem and add 1 to the gem counter on the HUD.
        if(collision.collider.tag == "Gem")
        {
            //NotifyObservers(PlayerEvents.CollectedGem);
            Debug.Log("Gem touched");
            Destroy(collision.collider.gameObject);
            gameManager.UpdateGemCount();

            // Checks if the desired amount of gems have been collected.
            gameManager.CheckWinCondition();
        }

        // When the player touches an enemy
        if (collision.collider.tag == "Enemy")
        {
            //NotifyObservers(PlayerEvents.ReceivedDamage);
            Debug.Log("Enemy touched");
            gameManager.UpdateHealth();

            // Checks if the player's health has reached zero.
            gameManager.CheckLoseCondition();
        }
        //Debug.Log("Object touched: " + collision.collider.name);
    }
}
