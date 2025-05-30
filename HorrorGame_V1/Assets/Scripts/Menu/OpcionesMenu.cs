using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importar el espacio de nombres para la gestión de escenas

/// <summary>
/// Clase para manejar opciones básicas del menú principal,
/// como iniciar el juego o salir de la aplicación.
/// </summary>
public class OpcionesMenu : MonoBehaviour
{
    /// <summary>
    /// Método para iniciar el juego cargando la escena indicada por nombre.
    /// </summary>
    /// <param name="Nombrenivel">Nombre de la escena a cargar.</param>
    public void IniciarJuego(string Nombrenivel)
    {
        SceneManager.LoadScene(Nombrenivel); // Cargar la escena del juego
    }

    /// <summary>
    /// Método para salir de la aplicación.
    /// </summary>
    public void Salir()
    {
        Application.Quit(); // Cerrar la aplicación
    }
}
