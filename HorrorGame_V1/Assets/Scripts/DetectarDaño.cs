using UnityEngine;

/// <summary>
/// Detecta colisiones con objetos etiquetados como "Ataque" y aplica daño al jugador.
/// </summary>
public class DetectarDaño : MonoBehaviour
{
    /// <summary>
    /// Cantidad de daño que se aplicará al jugador al detectar una colisión.
    /// </summary>
    public int dañoRecibido = 10;

    /// <summary>
    /// Se activa al entrar en contacto con otro collider.
    /// Si el objeto tiene la etiqueta "Ataque", aplica el daño al jugador mediante el GameManager.
    /// </summary>
    /// <param name="other">El collider del objeto que ha entrado en contacto.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ataque"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RecibirDaño(dañoRecibido);
            }
        }
    }
}
