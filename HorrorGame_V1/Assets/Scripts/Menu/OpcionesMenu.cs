using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importar el espacio de nombres para la gesti�n de escenas

/// <summary>
/// Clase para manejar opciones b�sicas del men� principal,
/// como iniciar el juego o salir de la aplicaci�n.
/// </summary>
public class OpcionesMenu : MonoBehaviour
{
    /// <summary>
    /// M�todo para iniciar el juego cargando la escena indicada por nombre.
    /// </summary>
    /// <param name="Nombrenivel">Nombre de la escena a cargar.</param>
    public void IniciarJuego(string Nombrenivel)
    {
        SceneManager.LoadScene(Nombrenivel); // Cargar la escena del juego
    }

    /// <summary>
    /// M�todo para salir de la aplicaci�n.
    /// </summary>
    public void Salir()
    {
        Application.Quit(); // Cerrar la aplicaci�n
    }
}
