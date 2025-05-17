using UnityEngine;

public class CuchilloHitbox : MonoBehaviour
{
    private BoxCollider hitboxAtaque;
    private BoxCollider colliderInteraccion; 

    private void Awake()
    {
        hitboxAtaque = GetComponent<BoxCollider>();
        colliderInteraccion = GetComponentInChildren<BoxCollider>(); // Asume que est� en un hijo

        hitboxAtaque.isTrigger = true;
        hitboxAtaque.enabled = false; // Desactivado inicialmente

        // Asegurarse que el collider de interacci�n est� activo solo cuando es recolectable
        if (GetComponent<ObjetoRecolectable>() != null)
        {
            colliderInteraccion.enabled = true;
        }
    }

    public void ActivarColision()
    {
        hitboxAtaque.enabled = true;
        if (colliderInteraccion != null) colliderInteraccion.enabled = false;
    }

    public void DesactivarColision()
    {
        hitboxAtaque.enabled = false;
    }

    // Nuevo m�todo para cuando el cuchillo es equipado
    public void DesactivarInteraccion()
    {
        if (colliderInteraccion != null) colliderInteraccion.enabled = false;
    }
}

