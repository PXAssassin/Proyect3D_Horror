using UnityEngine;

/// <summary>
/// Script encargado de generar enemigos en puntos específicos y volverlos a generar
/// una vez que todos han sido eliminados.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Prefab del enemigo que será instanciado.
    /// </summary>
    public GameObject enemigoPrefab;

    /// <summary>
    /// Arreglo de puntos en la escena donde los enemigos serán generados.
    /// </summary>
    public Transform[] puntosDeSpawn;

    /// <summary>
    /// Arreglo de referencias a los enemigos actualmente instanciados.
    /// </summary>
    private GameObject[] enemigosActuales;

    /// <summary>
    /// Inicializa el sistema de spawn y genera los enemigos iniciales.
    /// </summary>
    void Start()
    {
        enemigosActuales = new GameObject[puntosDeSpawn.Length];
        SpawnEnemigos();
    }

    /// <summary>
    /// Verifica si todos los enemigos han sido destruidos, y si es así, vuelve a generarlos.
    /// </summary>
    void Update()
    {
        bool todosDestruidos = true;
        foreach (GameObject enemigo in enemigosActuales)
        {
            if (enemigo != null)
            {
                todosDestruidos = false;
                break;
            }
        }

        if (todosDestruidos)
        {
            SpawnEnemigos();
        }
    }

    /// <summary>
    /// Genera un nuevo enemigo en cada punto de spawn y guarda sus referencias.
    /// </summary>
    void SpawnEnemigos()
    {
        for (int i = 0; i < puntosDeSpawn.Length; i++)
        {
            enemigosActuales[i] = Instantiate(enemigoPrefab, puntosDeSpawn[i].position, Quaternion.identity);
        }
    }
}
