using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab del obstáculo
    public Transform spawnPoint; // Punto de aparición
    public float spawnRate = 2f; // Cada cuántos segundos aparece un obstáculo
    public float speed = 5f; // Velocidad de los obstáculos

    void Start()
    {
        // Generar obstáculos de manera repetitiva
        InvokeRepeating(nameof(SpawnObstacle), 0f, spawnRate);
    }

    void SpawnObstacle()
    {
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoint.position, spawnPoint.rotation);
        newObstacle.GetComponent<ObstacleMovement>().speed = speed; // Asignar velocidad
    }
}
