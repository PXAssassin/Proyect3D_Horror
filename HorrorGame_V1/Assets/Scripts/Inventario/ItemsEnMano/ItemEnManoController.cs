using UnityEngine;

public class ItemEnManoController : MonoBehaviour
{
    public Transform socketItem; // ← aquí va el GameObject llamado itemEnMano

    private GameObject itemActualEnMano;
    void Update()
    {
        if (itemActualEnMano != null)
        {
            // CURAR si es un botiquín y presiona click
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

    void UsarItemCurativo(InventarioItemData item)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RecuperarVida(item.cantidadCura);
        }

        // Remover del inventario
        InventarioHotbar.Instancia.RemoverItemPorID(item.id);

        // Quitar de la mano
        LimpiarItemActual();
    }

    public void EquiparItem(GameObject prefab)
    {
        LimpiarItemActual();
        if (prefab != null)
        {
            itemActualEnMano = Instantiate(prefab, socketItem);
            itemActualEnMano.transform.localPosition = Vector3.zero;
            itemActualEnMano.transform.localRotation = Quaternion.identity;

            // Desactivar físicas si el objeto tiene el script de control
            var fisicas = itemActualEnMano.GetComponent<ItemFisicas>();
            if (fisicas != null)
            {
                fisicas.ActivarModoEnMano();
            }

            // Desactivar recolección si aplica
            var hitbox = itemActualEnMano.GetComponent<CuchilloHitbox>();
            if (hitbox != null)
            {
                hitbox.DesactivarInteraccion();
            }
        }
    }

    public void LimpiarItemActual()
    {
        if (itemActualEnMano != null)
        {
            Destroy(itemActualEnMano);
            itemActualEnMano = null;
        }
    }

    public GameObject ObtenerItemActual()
    {
        return itemActualEnMano;
    }
}
