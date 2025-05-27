using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f;
    public float attackRange = 2.0f;
    public float activationRange = 10.0f; // Distancia para que se active el enemigo
    public int vidaMaxima = 3;

    private int vidaActual;
    public Transform player;
    private Animator animator;
    private bool isActive = false; // Indica si el enemigo ya se activó

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("Player no encontrado en la escena.");

        animator = GetComponent<Animator>();
        vidaActual = vidaMaxima;

        // Ya no hace falta setear nada para el Idle,
        // pues el Animator debe iniciar en ese estado por defecto.
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isActive)
        {
            if (distanceToPlayer <= activationRange)
            {
                isActive = true;
                // No es necesario cambiar parámetros aquí
                // El enemigo pasará de Idle a movimiento o ataque según la lógica siguiente
            }
            else return; // Mientras no se active, no hace nada
        }

        if (distanceToPlayer <= attackRange)
        {
            animator.SetTrigger("Atacar");
        }
        else
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ArmaJugador"))
        {
            var itemData = other.GetComponent<ObjetoRecolectable>()?.datosItem;
            if (itemData != null && itemData.esArma)
            {
                RecibirDaño(itemData.daño);
            }
        }
    }
}
