using UnityEngine;

public class ItemFisicas : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Llamado cuando el objeto es equipado (en mano)
    public void ActivarModoEnMano()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (col != null)
        {
            // Si quieres que no colisione mientras está en la mano, pon true
            // col.isTrigger = true; 

            // Si quieres que siga colisionando con el entorno, pon false
            col.isTrigger = false;
        }
    }

    // Llamado cuando el objeto es soltado o droppeado en el mundo
    public void ActivarModoFisico()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        if (col != null)
        {
            col.isTrigger = false;
        }
    }
}
