using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador central del juego que maneja la vida del jugador, 
/// su visualización en pantalla y la transición a otras escenas.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Instancia única del GameManager (patrón Singleton).
    /// </summary>
    public static GameManager Instance;

    [Header("Vida del Jugador")]
    /// <summary>
    /// Vida máxima que puede tener el jugador.
    /// </summary>
    public int vidaMaxima = 100;

    /// <summary>
    /// Vida actual del jugador.
    /// </summary>
    public int vidaActual = 100;

    [Header("UI de Vida")]
    /// <summary>
    /// Referencia al componente de texto que muestra la vida en la interfaz.
    /// </summary>
    public TMP_Text textoVida;

    /// <summary>
    /// Inicializa el singleton y evita que se destruya al cambiar de escena.
    /// </summary>
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

    /// <summary>
    /// Establece la vida actual como la máxima al comenzar y actualiza el texto de vida.
    /// </summary>
    private void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarTextoVida();
    }

    /// <summary>
    /// Resta vida al jugador y actualiza la interfaz.
    /// Si la vida llega a cero, regresa al menú principal.
    /// </summary>
    /// <param name="cantidad">Cantidad de daño recibido.</param>
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

    /// <summary>
    /// Añade vida al jugador y actualiza la interfaz.
    /// </summary>
    /// <param name="cantidad">Cantidad de vida a recuperar.</param>
    public void RecuperarVida(int cantidad)
    {
        vidaActual += cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarTextoVida();
    }

    /// <summary>
    /// Asigna el componente TMP_Text desde fuera del GameManager.
    /// </summary>
    /// <param name="nuevoTexto">Nuevo texto de vida a mostrar.</param>
    public void AsignarTextoVida(TMP_Text nuevoTexto)
    {
        textoVida = nuevoTexto;
        ActualizarTextoVida();
    }

    /// <summary>
    /// Actualiza el texto de vida en la UI.
    /// </summary>
    private void ActualizarTextoVida()
    {
        if (textoVida != null)
        {
            textoVida.text = $"Vida: {vidaActual}%";
        }
    }

    /// <summary>
    /// Carga la escena principal del menú cuando el jugador muere.
    /// </summary>
    private void VolverAlMenu()
    {
        SceneManager.LoadScene("SceneMenu"); // Asegúrate de que este nombre coincida con la escena real
    }
}
