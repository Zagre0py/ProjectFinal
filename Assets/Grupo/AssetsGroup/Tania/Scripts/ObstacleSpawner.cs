using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab del obstáculo que se instanciará
    public Transform spawnPoint; // Punto donde aparecerán los obstáculos

    [Range(0, 10)] 
    public int maxRange;

    [Range(0,10)]
    public int minRange;

    public float speed; // Velocidad con la que se moverán los obstáculos

    void Start()
    {
        StartCoroutine(SpawnObstacles()); // Iniciar la corrutina que genera obstáculos de forma repetitiva
    }

    IEnumerator SpawnObstacles()
    {
        while (true) // Bucle infinito para generar obstáculos constantemente
        {
            float spawnRate = Random.Range(minRange, maxRange); // Genera un tiempo aleatorio para la siguiente aparición
            SpawnObstacle(); // Llama a la función que instancia un nuevo obstáculo
            yield return new WaitForSeconds(spawnRate); // Espera el tiempo generado antes de crear el siguiente obstáculo
        }
    }

    void SpawnObstacle()
    {
        // Instancia un nuevo obstáculo en la posición y rotación del spawnPoint
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoint.position, obstaclePrefab.transform.rotation);

        // Asigna la velocidad al componente ObstacleMovement del nuevo obstáculo
        newObstacle.GetComponent<ObstacleMovement>().speed = speed;
    }
}
