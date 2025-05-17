using UnityEngine;
using System;

public class InventarioHotbar : MonoBehaviour
{
    public static InventarioHotbar Instancia; //para permitir acceder al inventario desde cualquier script
    public int tamaño = 6; //tamaño del hotbar
    public InventarioItemSlot[] slots; //array donde se guarda el contenido dde cada slot de itemData + sucantidad
    public int indiceSeleccionado = 0; //cual slost es el que esta activo
    [Header("Referencia al sistema de item en mano")]
    public ItemEnManoController itemEnManoController;


    public Transform puntoDrop; //punto donde se dropeara el item que tenemos en el inventario
    public event Action OnCambioInventario; //evento que se dispara cuando algo cambia en la hotbar

    private void Awake()
    {
        Instancia = this;
        slots = new InventarioItemSlot[tamaño]; //asignacion del tamaño definido anteriormente
        for (int i = 0; i < tamaño; i++) // llenar el array con instancias vacia, para evitar errores
            slots[i] = new InventarioItemSlot();
    }
    private void Start()
    {
        ActualizarItemEnMano();
    }

    private void Update()
    {
        ManejarScrollMouse();
        ManejarDropearItem();
    }

    void ManejarScrollMouse()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");//captura la rueda del mouse
        //condicion para saber a que direccion cambiar 
        if (scroll > 0f) CambiarSeleccion(1);// si es mayor hacia la derecha
        else if (scroll < 0f) CambiarSeleccion(-1); // si es menor hacia la izquierda
    }

    void ManejarDropearItem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var slot = slots[indiceSeleccionado];
            if (!slot.EstaVacio)
            {
                // Instanciar el objeto en el mundo
                Vector3 posicionDrop = puntoDrop != null ? puntoDrop.position : transform.position + transform.forward * 1.5f;
                Instantiate(slot.itemData.prefab, posicionDrop, Quaternion.identity);
                slot.cantidad--;
                if (slot.cantidad <= 0) slot.itemData = null;

                OnCambioInventario?.Invoke();
            }
        }
    }

    public bool AgregarItem(InventarioItemData item)
    {
        // Si ya existe y es acumulable
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemData == item && item.maximoItem > 1)
            {
                slots[i].cantidad++;
                OnCambioInventario?.Invoke();
                return true;
            }
        }

        // Buscar espacio vacío
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].EstaVacio)
            {
                slots[i].itemData = item;
                slots[i].cantidad = 1;
                OnCambioInventario?.Invoke();
                return true;
            }
        }
        return false;
    }

    public void CambiarSeleccion(int direccion)
    {
        indiceSeleccionado = (indiceSeleccionado + direccion + slots.Length) % slots.Length; //calculo para saber a que item apuntar
        OnCambioInventario?.Invoke();
        ActualizarItemEnMano();
    }

    public void IntercambiarItems(int index1, int index2)
    {
        var temp = slots[index1];
        slots[index1] = slots[index2];
        slots[index2] = temp;
        OnCambioInventario?.Invoke();
    }

    public void ActualizarItemEnMano()
    {
        if (itemEnManoController == null) return;

        var slot = slots[indiceSeleccionado];
        if (!slot.EstaVacio)
            itemEnManoController.EquiparItem(slot.itemData.prefab);
        else
            itemEnManoController.LimpiarItemActual();
    }
    


}
