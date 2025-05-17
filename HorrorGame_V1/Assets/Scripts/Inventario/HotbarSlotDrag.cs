using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotbarSlotDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int slotIndex;
    private Image icono;
    private Canvas canvas;

    private void Start()
    {
        icono = transform.Find("itemIcon").GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (InventarioHotbar.Instancia.slots[slotIndex].EstaVacio) return;

        DragIcon.Instance.Mostrar(icono.sprite, eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragIcon.Instance.Mover(eventData.position);
    }

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
