using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAct : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public float mouseSensitivity = 2.0f;
    public float verticalRotationLimit = 90f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;

    private CharacterController controller;
    private Animator animacionErika;
    public Transform cameraTransform;

    private float rotationX = 0;
    private float velocidadY = 0f;
    private bool cursorActivo = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animacionErika = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        ControlCursor();

        if (!cursorActivo)
        {
            ControlCamara();
            ControlMovimiento();
        }
    }

    void ControlCursor()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            cursorActivo = !cursorActivo;
            Cursor.lockState = cursorActivo ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = cursorActivo;
        }
    }

    void ControlCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalRotationLimit, verticalRotationLimit);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    void ControlMovimiento()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // Preparar valores para animación
        float animVelX = inputX;
        float animVelY = inputZ;

        // Detectar si está corriendo
        bool corriendo = Input.GetKey(KeyCode.LeftShift) && inputZ > 0;

        // Ajustar velocidad real y valores del blend tree
        float velocidadActual = movementSpeed;

        if (corriendo)
        {
            velocidadActual *= 1.8f;
            animVelY = 2f;
        }
        else if (inputZ < 0)
        {
            velocidadActual *= 0.5f;
            animVelY = -1f;
        }
        else if (inputZ > 0)
        {
            animVelY = 1f;
        }

        // Calcular dirección
        Vector3 move = transform.right * inputX + transform.forward * inputZ;
        move *= velocidadActual;

        // Saltar
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocidadY = jumpForce;
            }
            else
            {
                velocidadY = -1f;
            }
        }
        else
        {
            velocidadY -= gravity * Time.deltaTime;
        }

        move.y = velocidadY;

        controller.Move(move * Time.deltaTime);

        // Enviar a Animator
        if (animacionErika != null)
        {
            animacionErika.SetFloat("VelX", animVelX);
            animacionErika.SetFloat("VelY", animVelY);
            animacionErika.SetBool("EnSuelo", controller.isGrounded);
            animacionErika.SetBool("Corriendo", corriendo);
        }
    }
}
