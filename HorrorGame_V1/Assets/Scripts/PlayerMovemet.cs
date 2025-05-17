using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemet : MonoBehaviour
{
    public float runspeed = 7f; // Speed of the player movement
    public float rotationSpeed = 250f; // Speed of the player rotation

    public Animator animator; // Reference to the Animator component

    private float x ,y; // Variables to store input values

    public Rigidbody rb; // Reference to the Rigidbody component
    public float jumpHeight = 0.6f;
    public Transform groundCheck; // Reference to the ground check object
    public float groundDistance = 0.1f; // Distance to check for ground}
    public LayerMask groundMask; // Layer mask to identify ground objects
    bool isGrounded; // Flag to check if the player is grounded


    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)
        y = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrow keys)

        transform.Rotate(0,x*Time.deltaTime*rotationSpeed ,0); //controla la rotacion del jugador
        transform.Translate(0, 0, y*Time.deltaTime*runspeed);

        animator.SetFloat("velX", x); // Set the "Speed" parameter in the Animator based on vertical input
        animator.SetFloat("velY", y); // Set the "Speed" parameter in the Animator based on horizontal input


        if(Input.GetKey("f")) // baile 1
        {
           animator.Play("Dance"); // Play the "Baile1" animation

            animator.SetBool("Other ", false); // cambiar el baile acaminar normal
        }   

        if (x> 0 || x < 0 || y > 0 || y < 0) // If there is any movement input
        {
            animator.SetBool("Other ", true); // cambiar el baile acaminar normal
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //mira si el personaje esta en el piso 

        if(Input.GetKey("space") && isGrounded) // If the space key is pressed and the player is grounded
        {
            animator.Play("Jump"); // Play the "Jump" animation
            Invoke("Jump", 1f); // un tiempo de espera para saltar

        }
    }
    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // resetea la velocidad vertical
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse); // luego aplica la fuerza de salto
    }
}
