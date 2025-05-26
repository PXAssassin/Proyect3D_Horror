using UnityEngine;
using System;

public class InventarioHotbar : MonoBehaviour
{
    public static InventarioHotbar Instancia; 
    public int tamaño = 6; 

    public InventarioItemSlot[] slots; //Varible de arreglos unidimensional

    public int indiceSeleccionado = 0; 
    [Header("Referencia al sistema de item en mano")]
    public ItemEnManoController itemEnManoController;
    public Transform puntoDrop; 
    public event Action OnCambioInventario; 

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        slots = new InventarioItemSlot[tamaño];
        for (int i = 0; i < tamaño; i++)
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
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) CambiarSeleccion(1);
        else if (scroll < 0f) CambiarSeleccion(-1); 
    }

    public void CambiarSeleccion(int direccion)
    {
        indiceSeleccionado = (indiceSeleccionado + direccion + slots.Length) % slots.Length;
        OnCambioInventario?.Invoke();
        ActualizarItemEnMano();
    }

    void ManejarDropearItem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var slot = slots[indiceSeleccionado];
            if (!slot.EstaVacio)
            {
                Vector3 posicionDrop = puntoDrop != null ? puntoDrop.position : transform.position + transform.forward * 1.5f;
                GameObject objetoDrop = Instantiate(slot.itemData.prefab, posicionDrop, Quaternion.identity);

                var fisicas = objetoDrop.GetComponent<ItemFisicas>();
                if (fisicas != null)
                {
                    fisicas.ActivarModoFisico();
                }

                slot.cantidad--;
                if (slot.cantidad <= 0)
                    slot.itemData = null;

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

    public bool TieneItemPorID(string id)
    {
        foreach (var slot in slots)
        {
            if (!slot.EstaVacio && slot.itemData.id == id)
                return true;
        }
        return false;
    }

    public void RemoverItemPorID(string id)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].EstaVacio && slots[i].itemData.id == id)
            {
                slots[i].cantidad--;
                if (slots[i].cantidad <= 0)
                    slots[i].itemData = null;

                OnCambioInventario?.Invoke();
                return;
            }
        }
    }

    public void ReasignarReferenciasEnEscena()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player1");

        if (player != null)
        {
            itemEnManoController = player.GetComponent<ItemEnManoController>();

            Transform nuevoDrop = GameObject.FindGameObjectWithTag("DropPoint")?.transform;
            if (nuevoDrop != null)
            {
                puntoDrop = nuevoDrop;
            }
        }
        else
        {
            Debug.LogWarning("Jugador no encontrado en la escena para vincular el inventario.");
        }
    }


}
