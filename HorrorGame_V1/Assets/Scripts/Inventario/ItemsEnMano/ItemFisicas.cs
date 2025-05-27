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

    // Llamado cuando el objeto es equipado
    public void ActivarModoEnMano()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (col != null)
        {
            col.isTrigger = false; // Opcional: si necesitas trigger en la mano o no
        }
    }

    // Llamado cuando el objeto se dropea
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
