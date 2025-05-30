using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la interfaz visual de la hotbar, mostrando los íconos de los items y resaltando el slot seleccionado.
/// </summary>
public class HotbarUI : MonoBehaviour
{
    /// <summary>
    /// Arreglo de GameObjects que representan los slots de la hotbar en la UI.
    /// Asignar en el inspector los objetos Slot_0, Slot_1, ..., Slot_5.
    /// </summary>
    public GameObject[] slotsUI;

    /// <summary>
    /// Color que se aplica al fondo del slot seleccionado.
    /// </summary>
    public Color colorSeleccionado = Color.green;

    /// <summary>
    /// Color normal del fondo para los slots no seleccionados.
    /// </summary>
    public Color colorNormal = Color.white;

    /// <summary>
    /// Inicializa la suscripción al evento de cambio en el inventario y actualiza la UI.
    /// </summary>
    private void Start()
    {
        InventarioHotbar.Instancia.OnCambioInventario += ActualizarHotbar;
        ActualizarHotbar();
    }

    /// <summary>
    /// Actualiza la interfaz gráfica de la hotbar, asignando los iconos correspondientes y el color del slot seleccionado.
    /// </summary>
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
