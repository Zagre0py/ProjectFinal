using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad del obstáculo
    public int direction = 1; // 1 = derecha, 2 = izquierda
    public bool directionSet= false;

    void Update()
    {
        // Verifica la dirección y ajusta el movimiento
        if (direction == 1)
        {
            transform.position += transform.right * speed * Time.deltaTime; // Derecha
        }
        else if (direction == 2)
        {
            transform.position -= transform.right * speed * Time.deltaTime; // Izquierda
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (directionSet == false)
        {
            if (other.gameObject.name == "LeftLimit")
            {
                directionSet = true;
                direction = 1;
            }
            if (other.gameObject.name == "RigthLimit")
            {
                directionSet = true;
                direction = 2;
            }
        }
        if(directionSet == true)
        {
            if(other.gameObject.name == "LeftDestoy" || other.gameObject.name == "RigthDestroy")
            {
                Destroy(gameObject);
            }
        }
    }
}
