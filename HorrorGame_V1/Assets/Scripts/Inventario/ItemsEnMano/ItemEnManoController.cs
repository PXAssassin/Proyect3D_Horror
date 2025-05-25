using UnityEngine;

public class ItemEnManoController : MonoBehaviour
{
    public Transform socketItem; // ← aquí va el GameObject llamado itemEnMano

    private GameObject itemActualEnMano;

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
