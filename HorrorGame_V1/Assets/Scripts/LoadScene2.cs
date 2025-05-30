using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Permite cargar una escena específica cuando el jugador entra en un trigger.
/// </summary>
public class LoadScene2 : MonoBehaviour
{
    /// <summary>
    /// Nombre de la escena que se cargará al activar el trigger.
    /// </summary>
    public string nombreEscena;

    /// <summary>
    /// Detecta cuando otro collider entra en el trigger.
    /// Si el objeto tiene la etiqueta "Player1", carga la escena especificada.
    /// </summary>
    /// <param name="other">Collider que entra en el trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            loadScene(nombreEscena);
        }
    }

    /// <summary>
    /// Cambia la escena actual por la indicada por el nombre.
    /// </summary>
    /// <param name="name">Nombre de la escena a cargar.</param>
    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
