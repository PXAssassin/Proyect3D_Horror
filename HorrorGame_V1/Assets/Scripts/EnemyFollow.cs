using UnityEngine;

/// <summary>
/// Script que permite a un enemigo seguir y atacar al jugador cuando este se encuentra en un rango determinado.
/// También gestiona la vida del enemigo y su destrucción al recibir daño.
/// </summary>
public class EnemyFollow : MonoBehaviour
{
    /// <summary>
    /// Velocidad de movimiento del enemigo.
    /// </summary>
    public float speed = 2.0f;

    /// <summary>
    /// Velocidad con la que el enemigo rota para mirar al jugador.
    /// </summary>
    public float rotationSpeed = 5.0f;

    /// <summary>
    /// Distancia a la que el enemigo empieza a atacar.
    /// </summary>
    public float attackRange = 2.0f;

    /// <summary>
    /// Distancia a la que el enemigo se activa y comienza a moverse.
    /// </summary>
    public float activationRange = 10.0f;

    /// <summary>
    /// Vida máxima del enemigo.
    /// </summary>
    public int vidaMaxima = 3;

    /// <summary>
    /// Vida actual del enemigo.
    /// </summary>
    private int vidaActual;

    /// <summary>
    /// Referencia al jugador.
    /// </summary>
    public Transform player;

    /// <summary>
    /// Referencia al componente Animator para manejar animaciones.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Indica si el enemigo ha sido activado.
    /// </summary>
    private bool isActive = false;

    /// <summary>
    /// Inicializa referencias y valores por defecto.
    /// </summary>
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("Player no encontrado en la escena.");

        animator = GetComponent<Animator>();
        vidaActual = vidaMaxima;
    }

    /// <summary>
    /// Controla la activación del enemigo, su movimiento y ataque hacia el jugador.
    /// </summary>
    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isActive)
        {
            if (distanceToPlayer <= activationRange)
            {
                isActive = true;
            }
            else return;
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

    /// <summary>
    /// Aplica daño al enemigo y lo destruye si su vida llega a cero.
    /// </summary>
    /// <param name="cantidad">Cantidad de daño a recibir.</param>
    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Detecta colisiones con armas del jugador y aplica daño si corresponde.
    /// </summary>
    /// <param name="other">Colisionador con el que se ha producido la colisión.</param>
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
