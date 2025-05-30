using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del jugador, la c�mara, el salto y la animaci�n.
/// Incluye sensibilidad del mouse, l�mites de rotaci�n vertical y manejo del cursor.
/// </summary>
public class PlayerMoveAct : MonoBehaviour
{
    /// <summary>Velocidad base de movimiento del jugador.</summary>
    public float movementSpeed = 2.0f;
    /// <summary>Sensibilidad del mouse para rotar la c�mara y el jugador.</summary>
    public float mouseSensitivity = 2.0f;
    /// <summary>L�mite m�ximo de rotaci�n vertical de la c�mara en grados.</summary>
    public float verticalRotationLimit = 90f;
    /// <summary>Fuerza aplicada para saltar.</summary>
    public float jumpForce = 8.0f;
    /// <summary>Valor de gravedad aplicado cuando el jugador est� en el aire.</summary>
    public float gravity = 20.0f;

    private CharacterController controller;
    private Animator animacionErika;
    /// <summary>Transform de la c�mara que se rota verticalmente.</summary>
    public Transform cameraTransform;

    private float rotationX = 0;
    private float velocidadY = 0f;
    private bool cursorActivo = false;

    /// <summary>
    /// Inicializa referencias y configura el cursor bloqueado e invisible.
    /// </summary>
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animacionErika = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// M�todo llamado cada frame para controlar cursor, c�mara y movimiento.
    /// </summary>
    void Update()
    {
        ControlCursor();

        if (!cursorActivo)
        {
            ControlCamara();
            ControlMovimiento();
        }
    }

    /// <summary>
    /// Alterna el estado del cursor entre visible y bloqueado al presionar F1.
    /// </summary>
    void ControlCursor()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            cursorActivo = !cursorActivo;
            Cursor.lockState = cursorActivo ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = cursorActivo;
        }
    }

    /// <summary>
    /// Controla la rotaci�n de la c�mara y el jugador usando la entrada del mouse.
    /// </summary>
    void ControlCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rota el jugador horizontalmente
        transform.Rotate(Vector3.up * mouseX);

        // Rota la c�mara verticalmente y limita el �ngulo
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalRotationLimit, verticalRotationLimit);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    /// <summary>
    /// Controla el movimiento del jugador, salto y env�a par�metros al Animator.
    /// </summary>
    void ControlMovimiento()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // Variables para la animaci�n (Blend Tree)
        float animVelX = inputX;
        float animVelY = inputZ;

        // Detectar si el jugador corre (Shift + avanzar)
        bool corriendo = Input.GetKey(KeyCode.LeftShift) && inputZ > 0;

        // Ajustar velocidad y valores de animaci�n seg�n la acci�n
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

        // Calcular vector de movimiento en plano horizontal
        Vector3 move = transform.right * inputX + transform.forward * inputZ;
        move *= velocidadActual;

        // Control del salto y gravedad
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocidadY = jumpForce;
            }
            else
            {
                velocidadY = -1f; // Mantener al jugador pegado al suelo
            }
        }
        else
        {
            velocidadY -= gravity * Time.deltaTime;
        }

        move.y = velocidadY;

        // Mover al jugador
        controller.Move(move * Time.deltaTime);

        // Enviar par�metros para controlar animaciones
        if (animacionErika != null)
        {
            animacionErika.SetFloat("VelX", animVelX);
            animacionErika.SetFloat("VelY", animVelY);
            animacionErika.SetBool("EnSuelo", controller.isGrounded);
            animacionErika.SetBool("Corriendo", corriendo);
        }
    }
}
