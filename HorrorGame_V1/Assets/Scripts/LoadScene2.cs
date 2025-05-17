using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene2 : MonoBehaviour
{
    public string nombreEscena;  // Nombre de la escena a cargar

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))  // Si el Player toca la caja
        {
            loadScene(nombreEscena);  // Llama a la función para cambiar de escena
        }
    }

    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);  // Cambia la escena según el nombre
    }
}
