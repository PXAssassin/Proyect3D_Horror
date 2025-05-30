using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Permite cambiar de escena cuando el jugador se acerca a una puerta
/// y tiene la llave requerida en su inventario.
/// </summary>
public class DoorChangeEscene : MonoBehaviour
{
    /// <summary>
    /// ID del objeto que el jugador debe tener en mano para abrir la puerta.
    /// </summary>
    [Tooltip("ID del objeto requerido en mano")]
    public string idLlaveRequerida = "";

    /// <summary>
    /// Nombre exacto de la escena que se cargar� al usar la llave.
    /// </summary>
    [Tooltip("Nombre exacto de la escena a cargar")]
    public string nombreEscenaDestino = " ";

    /// <summary>
    /// Indica si el jugador est� dentro del �rea de interacci�n de la puerta.
    /// </summary>
    private bool jugadorCerca = false;

    /// <summary>
    /// Verifica si el jugador est� cerca y presiona la tecla F.
    /// Si tiene la llave correcta, la remueve del inventario y carga la nueva escena.
    /// </summary>
    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F))
        {
            var slot = InventarioHotbar.Instancia.slots[InventarioHotbar.Instancia.indiceSeleccionado];
            if (!slot.EstaVacio && slot.itemData.id == idLlaveRequerida)
            {
                InventarioHotbar.Instancia.RemoverItemPorID(idLlaveRequerida);
                SceneManager.LoadScene(nombreEscenaDestino);
            }
        }
    }

    /// <summary>
    /// Se activa cuando el jugador entra en el �rea de colisi�n de la puerta.
    /// </summary>
    /// <param name="other">El collider del objeto que entr�.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")) jugadorCerca = true;
    }

    /// <summary>
    /// Se activa cuando el jugador sale del �rea de colisi�n de la puerta.
    /// </summary>
    /// <param name="other">El collider del objeto que sali�.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1")) jugadorCerca = false;
    }
}
