using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab del obstáculo que se instanciará
    public Transform spawnPoint; // Punto donde aparecerán los obstáculos
    public float speed = 5f; // Velocidad con la que se moverán los obstáculos

    void Start()
    {
        StartCoroutine(SpawnObstacles()); // Iniciar la corrutina que genera obstáculos de forma repetitiva
    }

    IEnumerator SpawnObstacles()
    {
        while (true) // Bucle infinito para generar obstáculos constantemente
        {
            float spawnRate = Random.Range(1f, 4f); // Genera un tiempo aleatorio entre 1 y 3 segundos para la siguiente aparición
            SpawnObstacle(); // Llama a la función que instancia un nuevo obstáculo
            yield return new WaitForSeconds(spawnRate); // Espera el tiempo generado antes de crear el siguiente obstáculo
        }
    }

    void SpawnObstacle()
    {
        // Instancia un nuevo obstáculo en la posición y rotación del spawnPoint
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoint.position, spawnPoint.rotation);

        // Asigna la velocidad al componente ObstacleMovement del nuevo obstáculo
        newObstacle.GetComponent<ObstacleMovement>().speed = speed;
    }
}
