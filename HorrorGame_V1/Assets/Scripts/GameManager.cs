using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Vida del Jugador")]
    public int vidaMaxima = 100;
    public int vidaActual = 100;

    [Header("UI de Vida")]
    public TMP_Text textoVida; // Referencia al TMP_Text dentro de txtVida

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarTextoVida();
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarTextoVida();

        if (vidaActual <= 0)
        {
            VolverAlMenu();
        }
    }

    public void RecuperarVida(int cantidad)
    {
        vidaActual += cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarTextoVida();
    }

    public void AsignarTextoVida(TMP_Text nuevoTexto)
    {
        textoVida = nuevoTexto;
        ActualizarTextoVida();
    }

    private void ActualizarTextoVida()
    {
        if (textoVida != null)
        {
            textoVida.text = $"Vida: {vidaActual}%";
        }
    }

    private void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu"); // Asegúrate que el nombre coincida con tu escena real
    }
}
