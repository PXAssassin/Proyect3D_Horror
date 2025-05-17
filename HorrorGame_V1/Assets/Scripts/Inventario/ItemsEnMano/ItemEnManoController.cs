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

            // Desactivar el sistema de recolección cuando está equipado
            var hitbox = itemActualEnMano.GetComponent<CuchilloHitbox>();
            if (hitbox != null) hitbox.DesactivarInteraccion();
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
