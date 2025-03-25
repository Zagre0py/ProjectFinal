using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad a la que se moverá el obstáculo
    public int direction = 1; // Dirección del movimiento (1 = derecha, 2 = izquierda)
    public bool directionSet = false; // Indica si la dirección ya ha sido establecida

    void Update()
    {
        // Verifica la dirección del obstáculo y ajusta su movimiento en consecuencia
        if (direction == 1)
        {
            transform.position += transform.right * speed * Time.deltaTime; // Se mueve hacia la derecha
        }
        else if (direction == 2)
        {
            transform.position -= transform.right * speed * Time.deltaTime; // Se mueve hacia la izquierda
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si la dirección aún no ha sido establecida, se define según el objeto con el que colisiona
        if (!directionSet)
        {
            if (other.gameObject.name == "LeftLimit") // Si toca el límite izquierdo
            {
                directionSet = true;
                direction = 1; // Se mueve hacia la derecha
            }
            if (other.gameObject.name == "RigthLimit") // Si toca el límite derecho
            {
                directionSet = true;
                direction = 2; // Se mueve hacia la izquierda
            }
        }

        // Si la dirección ya ha sido establecida, se verifica si debe ser destruido
        if (directionSet)
        {
            if (other.gameObject.name == "LeftDestoy" || other.gameObject.name == "RigthDestroy")
            {
                Destroy(gameObject); // Destruye el obstáculo si alcanza el límite de destrucción
            }
        }
    }
}