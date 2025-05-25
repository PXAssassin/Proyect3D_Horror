using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f;
    public float attackRange = 2.0f;
    public int vidaMaxima = 3;

    private int vidaActual;
    private Transform player;
    private Animator animator;

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

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

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
            // Aquí puedes poner animación de muerte si deseas
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
