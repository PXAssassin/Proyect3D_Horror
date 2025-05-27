using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorChangeEscene : MonoBehaviour
{
   

    [Tooltip("ID del objeto requerido en mano")]
    public string idLlaveRequerida = "";

    [Tooltip("Nombre exacto de la escena a cargar")]
    public string nombreEscenaDestino = " ";

    private bool jugadorCerca = false;
   
    void Update()
    {

        if (jugadorCerca && (Input.GetKeyDown(KeyCode.F)))
        {
            var slot = InventarioHotbar.Instancia.slots[InventarioHotbar.Instancia.indiceSeleccionado];
            if (!slot.EstaVacio && slot.itemData.id == idLlaveRequerida)
            {
                InventarioHotbar.Instancia.RemoverItemPorID(idLlaveRequerida);
                SceneManager.LoadScene(nombreEscenaDestino);
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")) jugadorCerca = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1")) jugadorCerca = false;
    }
}
