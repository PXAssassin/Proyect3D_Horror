using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public Transform[] puntosDeSpawn;

    private GameObject[] enemigosActuales;

    void Start()
    {
        enemigosActuales = new GameObject[puntosDeSpawn.Length];
        SpawnEnemigos();
    }

    void Update()
    {
        // Verifica si todos los enemigos han sido destruidos
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

    void SpawnEnemigos()
    {
        for (int i = 0; i < puntosDeSpawn.Length; i++)
        {
            enemigosActuales[i] = Instantiate(enemigoPrefab, puntosDeSpawn[i].position, Quaternion.identity);
        }
    }
}
