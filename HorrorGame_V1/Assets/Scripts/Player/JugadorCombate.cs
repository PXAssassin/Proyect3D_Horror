using UnityEngine;

/// <summary>
/// Controla las acciones de combate del jugador,
/// espec�ficamente la animaci�n de ataque cuando se usa un arma.
/// </summary>
public class JugadorCombate : MonoBehaviour
{
    /// <summary>
    /// Referencia al componente Animator para controlar animaciones.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// M�todo llamado cada frame para detectar entrada y controlar la animaci�n de ataque.
    /// </summary>
    void Update()
    {
        // Obtener el slot seleccionado actualmente en el inventario hotbar
        var slot = InventarioHotbar.Instancia.slots[InventarioHotbar.Instancia.indiceSeleccionado];

        // Verificar que el slot no est� vac�o y que el item sea un arma
        if (!slot.EstaVacio && slot.itemData.esArma)
        {
            // Si se presiona el bot�n izquierdo del mouse, activar animaci�n de ataque
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("AtacarCuchillo");
            }
        }
    }
}
