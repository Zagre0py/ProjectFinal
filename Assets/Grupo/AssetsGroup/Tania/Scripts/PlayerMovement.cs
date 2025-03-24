using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveDistance = 1f;  // Distancia de cada paso
    public float moveSpeed = 5f;     // Velocidad del movimiento

    private Rigidbody rb;            // Referencia al Rigidbody del jugador
    private Vector3 targetPosition;  // Posición a la que queremos movernos
    private Vector3 spawnPoint;      // Posición inicial
    private bool isMoving = false;   // Variable para evitar moverse hasta terminar el movimiento anterior

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody del jugador
        targetPosition = transform.position; // Inicializar la posición objetivo
        spawnPoint = transform.position; //Guardar la Posición inicial
    }

    void Update()
    {
        // Solo permitir movimiento si no estamos ya en medio de un desplazamiento
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W)) Move(Vector3.forward); // Mover adelante
            if (Input.GetKeyDown(KeyCode.S)) Move(Vector3.back);    // Mover atrás
            if (Input.GetKeyDown(KeyCode.A)) Move(Vector3.left);    // Mover izquierda
            if (Input.GetKeyDown(KeyCode.D)) Move(Vector3.right);   // Mover derecha
        }
    }

    void FixedUpdate()
    {
        // Si estamos en movimiento, actualizar la posición con MovePosition()
        if (isMoving)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));

            // Si el jugador ha llegado al destino, detener el movimiento
            if (Vector3.Distance(rb.position, targetPosition) < 0.01f)
            {
                rb.position = targetPosition; // Ajustar la posición para evitar errores de precisión
                isMoving = false; // Permitir el siguiente movimiento
            }
        }
    }

    void Move(Vector3 direction)
    {
        // Cambia la posición objetivo en la dirección deseada
        targetPosition += direction * moveDistance;
        isMoving = true; // Marcar que el jugador está en movimiento
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "water")
        {
            transform.position = spawnPoint;
            targetPosition = transform.position;
        }

        if (collision.gameObject.name == "wood(Clone)")
        {
            gameObject.transform.SetParent(collision.transform, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "wood(Clone)")
        {
            gameObject.transform.SetParent(null); // Deja de ser hijo del tronco
        }
    }


}
