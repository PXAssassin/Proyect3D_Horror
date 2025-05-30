using UnityEngine;

/// <summary>
/// Controla el objeto que el jugador tiene en la mano,
/// permitiendo equipar, usar (como consumir un botiquín) y limpiar el objeto actual.
/// </summary>
public class ItemEnManoController : MonoBehaviour
{
    [Tooltip("Transform donde se anclará el objeto en mano, generalmente un socket en la mano del jugador.")]
    public Transform socketItem; // El GameObject hijo donde se instancia el item

    private GameObject itemActualEnMano;

    void Update()
    {
        if (itemActualEnMano != null)
        {
            // Usar el item si es curativo y el jugador presiona click izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                var recolectable = itemActualEnMano.GetComponent<ObjetoRecolectable>();
                if (recolectable != null && recolectable.datosItem.esCurativo)
                {
                    UsarItemCurativo(recolectable.datosItem);
                }
            }
        }
    }

    /// <summary>
    /// Aplica la cura al jugador y remueve el item del inventario y la mano.
    /// </summary>
    /// <param name="item">Datos del item curativo.</param>
    void UsarItemCurativo(InventarioItemData item)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RecuperarVida(item.cantidadCura);
        }

        // Remueve el item del inventario por su ID
        InventarioHotbar.Instancia.RemoverItemPorID(item.id);

        // Elimina el objeto visual de la mano
        LimpiarItemActual();
    }

    /// <summary>
    /// Instancia un nuevo item en la mano, destruyendo el anterior si existe.
    /// </summary>
    /// <param name="prefab">Prefab del item a equipar.</param>
    public void EquiparItem(GameObject prefab)
    {
        LimpiarItemActual();

        if (prefab != null)
        {
            itemActualEnMano = Instantiate(prefab, socketItem);
            itemActualEnMano.transform.localPosition = Vector3.zero;
            itemActualEnMano.transform.localRotation = Quaternion.identity;

            // Ajustar físicas para modo "en mano"
            var fisicas = itemActualEnMano.GetComponent<ItemFisicas>();
            if (fisicas != null)
            {
                fisicas.ActivarModoEnMano();
            }

            // Desactivar interacción si es un arma o similar
            var hitbox = itemActualEnMano.GetComponent<CuchilloHitbox>();
            if (hitbox != null)
            {
                hitbox.DesactivarInteraccion();
            }
        }
    }

    /// <summary>
    /// Elimina el objeto actualmente en mano.
    /// </summary>
    public void LimpiarItemActual()
    {
        if (itemActualEnMano != null)
        {
            Destroy(itemActualEnMano);
            itemActualEnMano = null;
        }
    }

    /// <summary>
    /// Devuelve el objeto actualmente equipado en mano.
    /// </summary>
    /// <returns>GameObject del item en mano o null si no hay ninguno.</returns>
    public GameObject ObtenerItemActual()
    {
        return itemActualEnMano;
    }
}
