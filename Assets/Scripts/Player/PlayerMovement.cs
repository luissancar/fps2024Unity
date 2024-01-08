using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed;

    // Gravedad
    public float gravity = -9.8f;
    private Vector3 velocity;

    //GroundCheck
    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;
    public bool isGrounded;

    // Salto
    public float junpHeight = 300f;

    //Correr
    public bool isSprinting = false;
    public float sprintingSpeedMultiplier = 1.5F;
    public float sprintSpeed = 1; // no corremos


    //Stamina
    public float staminaUseAmount = 5;
    private StaminaBar staminaSlider;

    private void Start() //añadido con stamina
    {
        staminaSlider = FindObjectOfType<StaminaBar>();
    }

    void Update()
    {
        if (GameManager.instance.muerto)
            return;
        //Movimiento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime * sprintSpeed); //añadido

        //Gravedad
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        //GroundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position,
            sphereRadius, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(junpHeight * -2 * gravity * Time.deltaTime);
        }

        //Correr
        RunCheck();
    }

    public void RunCheck()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isSprinting = !isSprinting;
            if (isSprinting)
            {
                sprintSpeed = sprintingSpeedMultiplier;
                staminaSlider.UseStamina(staminaUseAmount);
            }
            else
            {
                sprintSpeed = 1;
                staminaSlider.UseStamina(0);
            }
        }

        if (isSprinting)
        {
            sprintSpeed = sprintingSpeedMultiplier;
        }
        else
        {
            sprintSpeed = 1;
        }
    }
}