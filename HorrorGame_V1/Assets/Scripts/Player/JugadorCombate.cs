using UnityEngine;

public class JugadorCombate : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        var slot = InventarioHotbar.Instancia.slots[InventarioHotbar.Instancia.indiceSeleccionado];

        if (!slot.EstaVacio && slot.itemData.esArma)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Lanza la animación que el item especifica
                animator.SetTrigger("AtacarCuchillo");
            }
        }
    }
}

