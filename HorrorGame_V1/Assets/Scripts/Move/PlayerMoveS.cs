using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMoveS : MonoBehaviour
{
    //Cursor
    private bool cursorActivo = false;
    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public float verticalRotationLimit = 90f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;

    private float rotationX = 0;
    private float VelocidadCaida = 0;

    private CharacterController controller;
    private Animator animacionErika;
    public Transform cameraTransform;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animacionErika = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            cursorActivo = !cursorActivo;
            Cursor.lockState = cursorActivo ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = cursorActivo;
        }

        if (!cursorActivo)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            transform.Rotate(Vector3.up * mouseX);

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -verticalRotationLimit, verticalRotationLimit);
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }

        ////___________Player------------------
        //// Tenga en cuenta que este movimieno no afecta a la camara solo a el Player, la camara se mueve a la par con el personaje | Configure bien la posicion de la camara
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //// mouseY obtiene valores al mover el mouse de arriba a abajo
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        ////tomamos el trasnform de nuestro personaje y le aplicamos los valores que obtenemos de mover el mouse en X
        //transform.Rotate(Vector3.up * mouseX);
        ////___________Player------------------


        ////------------------Camara------------------<
        ////Con mouseY
        //rotationX -= mouseY;
        ////Por si acaso = Mathf.Clamp Limita valores, que valroes va a limitar? --> Los mismos de rotationX
        //rotationX = Mathf.Clamp(rotationX, -verticalRotationLimit, verticalRotationLimit); // a que valores? a los definidos previamente anteriormente
        ////este movimiento obtenido del movimiento en Y, se lo asignamos solo a la camara| La camara entonces tendra una rotacion libre
        ////en el eje X | osea tendra una rotacion de 180º
        //cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //------------------Camara------------------<



        //------------------Player------------------<
        // moveX Esta asignado para A y D asi que cuando presionamos esas teclas tenemos los siguientes valores -1 o 1
        float moveX = Input.GetAxis("Horizontal");
        // moveZ Algo similar a lo anterior pero para W y S
        float moveZ = Input.GetAxis("Vertical");
        // aqui crearemos un vector que estara modificando sus valores (x,y,z) constantemente
        //transform.right hace referencia a (x,0,0)
        //transform.forward hace referencia a (0,0,z)
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move *= movementSpeed;

        //------------------Player------------------<
        //Atravez del CharacterController verificaremoos si el Controllador esta "Colisionando con algo"
        if (controller.isGrounded)
        {
            VelocidadCaida = -1f;  // Leve empuje para mantener al personaje pegado al suelo

            if (Input.GetButtonDown("Jump"))  // Espacio por defecto
            {
                VelocidadCaida = jumpForce;
            }
        }
        else
        {
            VelocidadCaida -= gravity * Time.deltaTime;
        }

        move.y = VelocidadCaida;

        controller.Move(move * Time.deltaTime);

        // Animaciones
        if (animacionErika != null)
        {
            animacionErika.SetFloat("VelX", moveX);
            animacionErika.SetFloat("VelY", moveZ);
            animacionErika.SetBool("EnSuelo", controller.isGrounded);
        }
    }
}
