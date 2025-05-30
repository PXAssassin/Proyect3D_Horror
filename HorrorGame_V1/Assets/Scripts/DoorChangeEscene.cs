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
    /// Nombre exacto de la escena que se cargará al usar la llave.
    /// </summary>
    [Tooltip("Nombre exacto de la escena a cargar")]
    public string nombreEscenaDestino = " ";

    /// <summary>
    /// Indica si el jugador está dentro del área de interacción de la puerta.
    /// </summary>
    private bool jugadorCerca = false;

    /// <summary>
    /// Verifica si el jugador está cerca y presiona la tecla F.
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
    /// Se activa cuando el jugador entra en el área de colisión de la puerta.
    /// </summary>
    /// <param name="other">El collider del objeto que entró.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")) jugadorCerca = true;
    }

    /// <summary>
    /// Se activa cuando el jugador sale del área de colisión de la puerta.
    /// </summary>
    /// <param name="other">El collider del objeto que salió.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1")) jugadorCerca = false;
    }
}
