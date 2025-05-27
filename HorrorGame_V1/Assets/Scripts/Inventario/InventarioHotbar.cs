using UnityEngine;
using System;
/// <summary>
/// Este es el "Manager" del inventario o el que maneja y procesa la logica de
/// Almacenar los objetos que recolectamos por el escenario para luego enviar esta informacion
/// a la Interfaz del Inventario.
/// </summary>

public class InventarioHotbar : MonoBehaviour
{
    public static InventarioHotbar Instancia; //Se instancia para que otros scripts ejecuten sus funciones cuando sea requerido
    public int tamaño = 6; //Se definieron 6 tamaños dessde el inicio

    public InventarioItemSlot[] slots; // variable de tipo Array para contener los obj de tipo In..ItemSlot

    public int indiceSeleccionado = 0; 

    [Header("Referencia Para Objeto que instancia los objetos en la mano:p")]
    public ItemEnManoController itemEnManoController;
    public Transform puntoDrop; //Objeto Vacio que sera el punto del drop 
    public event Action OnCambioInventario; //accion que nos permitira

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

        /// <summary>
        /// Inicializamos todas las posiciones con objetos
        /// de tipo itemSlot pero vacion en informacion del itemData
        /// Esto para poder implementar la logica de guardado
        /// </summary>
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
    /// <summary>
    /// Podremos navegar en la interfas a traves del evento
    /// de "Mouse ScrollWheel" que dependiendo de hacia que direccion
    /// scroleamos si es arriba sera 1 y abajo -1 esto bueno enviandole
    /// el valor a el Metodo Funcion "CambiarSeleccion" dependiendo si es >0 o < 0
    /// </summary>
    void ManejarScrollMouse()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) CambiarSeleccion(1);
        else if (scroll < 0f) CambiarSeleccion(-1); 
    }
    /// <summary>
    /// Recibe un valor: -1 o 1
    /// Hacemos una pequeña operacion "Circular" esto con el fin de saber
    /// hacia que slot nos deslizamos y para que del ultimo slot pasar a el primero
    /// notifica a la UI de los cambios que se estan haciendo y poder tener visualmente
    /// los cambios en pantalla
    /// </summary>
    public void CambiarSeleccion(int direccion)
    {
        indiceSeleccionado = (indiceSeleccionado + direccion + slots.Length) % slots.Length;
        OnCambioInventario?.Invoke();
        ActualizarItemEnMano();
    }


    /// <summary>
    /// Sirve para desacernos del item que tenemos en el inventario dependiendo
    /// de que slot estemos seleccionando
    /// </summary>
    void ManejarDropearItem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var slot = slots[indiceSeleccionado];
            if (!slot.EstaVacio)
            {
                Vector3 posicionDrop = puntoDrop.position;
                //Spawneara el objeto desde el puntoDrop quie le pasemos, dependiendo de lo que se encuentre en su propiedad prefab
                GameObject objetoDrop = Instantiate(slot.itemData.prefab, posicionDrop, Quaternion.identity);

                //Del objeto que dropeo accedemos a su activador de fisicas y activamos sus fisicas 
                var fisicas = objetoDrop.GetComponent<ItemFisicas>();
                if (fisicas != null)
                {
                    fisicas.ActivarModoFisico();
                }
                //por si le implementabamos cantidad pero nel
                slot.cantidad--;
                if (slot.cantidad <= 0)
                    slot.itemData = null;

                OnCambioInventario?.Invoke();
            }
        }
    }

    /// <summary>
    /// Para agregar item y verificar si tenemoss un espacio vacio
    /// o si tenemos el item y es acumulable, funcion no tiene uso, pero se deja para demostrar
    /// que es minimamente escalable
    /// </summary>
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
    /// <summary>
    /// Funcion para intercambiar los items en la interfaz 
    /// pero esta es la parte tecnica/logica para modificar la posicion
    /// de los slots
    /// </summary>
    public void IntercambiarItems(int index1, int index2)
    {
        var temp = slots[index1];
        slots[index1] = slots[index2];
        slots[index2] = temp;
        OnCambioInventario?.Invoke();
    }

    /// <summary>
    /// Como constantemente estaremos scoleando tendremos que quitar
    /// y poner el objeto en la mano del personaje
    /// ActualizarItemEnMano nos ayuda a ejecutar instrucciones del
    /// script que contiene el objeto donde se acopla el prefab 
    /// del slot seleccionado
    /// </summary>
    public void ActualizarItemEnMano()
    {
        if (itemEnManoController == null) return;

        var slot = slots[indiceSeleccionado];
        if (!slot.EstaVacio)
            itemEnManoController.EquiparItem(slot.itemData.prefab);
        else
            itemEnManoController.LimpiarItemActual();
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
