using UnityEngine;

public class JugadorAnimacionEventos : MonoBehaviour
{
    public ItemEnManoController itemEnManoController;

    public void ActivarColision()
    {
        var item = itemEnManoController.ObtenerItemActual();
        if (item != null)
        {
            var hitbox = item.GetComponent<CuchilloHitbox>();
            if (hitbox != null)
                hitbox.ActivarColision();
        }
    }

    public void DesactivarColision()
    {
        var item = itemEnManoController.ObtenerItemActual();
        if (item != null)
        {
            var hitbox = item.GetComponent<CuchilloHitbox>();
            if (hitbox != null)
                hitbox.DesactivarColision();
        }
    }
}
