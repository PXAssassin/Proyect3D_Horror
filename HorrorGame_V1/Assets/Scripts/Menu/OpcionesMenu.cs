using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importar el espacio de nombres para la gestión de escenas

public class OpcionesMenu : MonoBehaviour
{
    public void IniciarJuego(string Nombrenivel ) // Metodo para iniciar el juego
    { 
        SceneManager.LoadScene(Nombrenivel); // Cargar la escena del juego
    }
    public void Salir()
    {
        Application.Quit(); // Cerrar la aplicacion 
    }
}
