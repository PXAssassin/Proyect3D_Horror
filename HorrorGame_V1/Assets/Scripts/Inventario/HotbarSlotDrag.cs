using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Controla el drag and drop (arrastrar y soltar) de los slots del inventario tipo hotbar.
/// Permite iniciar, mover y finalizar el arrastre de los ítems en la hotbar.
/// </summary>
public class HotbarSlotDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// Índice del slot dentro del inventario hotbar.
    /// </summary>
    public int slotIndex;

    /// <summary>
    /// Imagen que representa el icono del item en este slot.
    /// </summary>
    private Image icono;

    /// <summary>
    /// Canvas padre para cálculo correcto de posiciones UI.
    /// </summary>
    private Canvas canvas;

    /// <summary>
    /// Inicializa referencias a componentes necesarios.
    /// </summary>
    private void Start()
    {
        icono = transform.Find("itemIcon").GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
    }

    /// <summary>
    /// Evento llamado al comenzar a arrastrar un slot.
    /// Muestra el icono del item si no está vacío.
    /// </summary>
    /// <param name="eventData">Datos del evento de pointer.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (InventarioHotbar.Instancia.slots[slotIndex].EstaVacio) return;

        DragIcon.Instance.Mostrar(icono.sprite, eventData.position);
    }

    /// <summary>
    /// Evento llamado mientras se arrastra el icono.
    /// Actualiza la posición del icono drag.
    /// </summary>
    /// <param name="eventData">Datos del evento de pointer.</param>
    public void OnDrag(PointerEventData eventData)
    {
        DragIcon.Instance.Mover(eventData.position);
    }

    /// <summary>
    /// Evento llamado al finalizar el arrastre.
    /// Oculta el icono drag y realiza intercambio de items si se suelta sobre otro slot válido.
    /// </summary>
    /// <param name="eventData">Datos del evento de pointer.</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        DragIcon.Instance.Ocultar();

        if (eventData.pointerEnter != null)
        {
            var otro = eventData.pointerEnter.GetComponentInParent<HotbarSlotDrag>();
            if (otro != null && otro.slotIndex != slotIndex)
            {
                InventarioHotbar.Instancia.IntercambiarItems(slotIndex, otro.slotIndex);
            }
        }
    }
}
