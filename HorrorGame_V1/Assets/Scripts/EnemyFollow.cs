using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2.0f;           // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5.0f;   // Velocidad de rotaci�n para seguir al jugador
    public float attackRange = 2.0f;     // Distancia a la que el enemigo atacar� al jugador

    private Transform player;
    private Animator animator;

    void Start()
    {
        // Encuentra al jugador por su tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
        if (playerObj != null)
        {

            player = playerObj.transform;

           
        }
        else
        {
            Debug.LogWarning("Player no encontrado en la escena.");
        }

        // Obt�n el componente Animator del enemigo
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calcula la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Si est� dentro del rango de ataque, realiza la animaci�n de ataque
            if (distanceToPlayer <= attackRange)
            {
                // Activar la animaci�n de ataque
                animator.SetTrigger("Atacar");
            }
            else
            {
                // Movimiento y rotaci�n hacia el jugador
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                // Rotaci�n hacia el jugador
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
