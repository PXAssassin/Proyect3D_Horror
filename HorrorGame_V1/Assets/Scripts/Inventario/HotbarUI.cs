using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    public GameObject[] slotsUI; // Asignar desde el inspector: Slot_0, Slot_1, ..., Slot_5
    public Color colorSeleccionado = Color.green;
    public Color colorNormal = Color.white;

    private void Start()
    {
        InventarioHotbar.Instancia.OnCambioInventario += ActualizarHotbar;
        ActualizarHotbar();
    }

    public void ActualizarHotbar()
    {
        for (int i = 0; i < slotsUI.Length; i++)
        {
            var slot = InventarioHotbar.Instancia.slots[i];
            Image icono = slotsUI[i].transform.Find("itemIcon").GetComponent<Image>();

            if (!slot.EstaVacio)
            {
                icono.enabled = true;
                icono.sprite = slot.itemData.icono;
            }
            else
            {
                icono.enabled = false;
                icono.sprite = null;
            }

            Image fondo = slotsUI[i].GetComponent<Image>();
            bool esSeleccionado = (i == InventarioHotbar.Instancia.indiceSeleccionado);
            fondo.color = esSeleccionado ? colorSeleccionado : colorNormal;
        }
    }
}
