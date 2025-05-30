using UnityEngine;

/// <summary>
/// Controla eventos relacionados con las animaciones del jugador,
/// espec�ficamente para activar o desactivar la colisi�n del arma en mano.
/// </summary>
public class JugadorAnimacionEventos : MonoBehaviour
{
    /// <summary>
    /// Referencia al controlador del item en mano para acceder al objeto equipado.
    /// </summary>
    public ItemEnManoController itemEnManoController;

    /// <summary>
    /// Activa la colisi�n del hitbox del arma que el jugador tenga equipada en la mano.
    /// Este m�todo normalmente es llamado desde eventos de animaci�n en el Animator.
    /// </summary>
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

    /// <summary>
    /// Desactiva la colisi�n del hitbox del arma que el jugador tenga equipada en la mano.
    /// Normalmente llamado para evitar da�o fuera del momento del ataque.
    /// </summary>
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
