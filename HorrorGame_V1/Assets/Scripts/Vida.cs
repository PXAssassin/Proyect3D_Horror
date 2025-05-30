using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Clase que se encarga de asignar el componente de texto que muestra la vida del jugador.
/// </summary>
public class Vida : MonoBehaviour
{
    /// <summary>
    /// Referencia al texto de TMP (TextMeshPro) donde se mostrará la vida.
    /// </summary>
    public TMP_Text textoVida;

    /// <summary>
    /// Método llamado al iniciar el objeto.
    /// Asigna el texto de vida al GameManager para que pueda actualizarlo.
    /// </summary>
    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AsignarTextoVida(textoVida);
        }
    }
}
