using UnityEngine;

public class CuchilloHitbox : MonoBehaviour
{
    private BoxCollider hitboxAtaque;

    private void Awake()
    {
        hitboxAtaque = GetComponent<BoxCollider>();

        if (hitboxAtaque != null)
        {
            hitboxAtaque.isTrigger = true;
            hitboxAtaque.enabled = false; // Desactivado al inicio
        }
    }

    public void ActivarColision()
    {
        if (hitboxAtaque != null)
            hitboxAtaque.enabled = true;
    }

    public void DesactivarColision()
    {
        if (hitboxAtaque != null)
            hitboxAtaque.enabled = false;
    }

    public void DesactivarInteraccion()
    {
        if (hitboxAtaque != null)
            hitboxAtaque.enabled = false;
    }
}
