using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb; // Referencia al Rigidbody del jugador
    private Vector3 targetPosition; // Posición objetivo a la que se moverá el jugador
    private Vector3 spawnPoint; // Punto de reaparición si el jugador cae al agua
    private Vector3 currentPosition; // Posición actual del jugador
    private Vector3 lastPlatformPosition; // Guarda la última posición de la plataforma para calcular su movimiento

    public float moveDistance = 1f; // Distancia de cada paso
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador

    private bool isMoving = false; // Indica si el jugador está en movimiento
    [SerializeField] private bool inPlatform = false; // Indica si el jugador está sobre una plataforma

    private GameObject currentPlatform; // Referencia a la plataforma en la que se encuentra el jugador

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtiene el componente Rigidbody
        targetPosition = transform.position; // Inicializa la posición objetivo con la posición inicial
        spawnPoint = transform.position; // Guarda la posición inicial como punto de reaparición
        currentPosition = transform.position; // Inicializa la posición actual con la posición inicial
    }

    void Update()
    {
        // Si no está en movimiento, permite que el jugador se mueva en la dirección deseada
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W)) Move(Vector3.forward); // Mover hacia adelante
            if (Input.GetKeyDown(KeyCode.S)) Move(Vector3.back); // Mover hacia atrás
            if (Input.GetKeyDown(KeyCode.A)) Move(Vector3.left); // Mover hacia la izquierda
            if (Input.GetKeyDown(KeyCode.D)) Move(Vector3.right); // Mover hacia la derecha
        }
    }

    void FixedUpdate()
    {
        // Si el jugador está en movimiento, lo mueve progresivamente hasta la posición deseada
        if (isMoving)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, currentPosition, moveSpeed * Time.fixedDeltaTime));

            // Si el jugador ha alcanzado la posición objetivo, detiene el movimiento
            if (Vector3.Distance(rb.position, currentPosition) < 0.01f)
            {
                rb.position = currentPosition; // Ajusta la posición final
                isMoving = false; // Detiene el movimiento
            }
        }

        // Si el jugador está sobre una plataforma, debe seguir su movimiento
        if (inPlatform && currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.transform.position - lastPlatformPosition; // Calcula el movimiento de la plataforma
            currentPosition += platformMovement; // Ajusta la posición del jugador en base al desplazamiento de la plataforma
            rb.MovePosition(currentPosition); // Aplica el movimiento ajustado al jugador
            lastPlatformPosition = currentPlatform.transform.position; // Actualiza la última posición de la plataforma
        }
    }

    void Move(Vector3 direction)
    {
        targetPosition = direction * moveDistance; // Calcula la nueva posición objetivo con la distancia de movimiento
        currentPosition += targetPosition; // Actualiza la posición actual sumando el desplazamiento
        isMoving = true; // Activa el estado de movimiento
    }

    private void OnCollisionStay(Collision collision)
    {
        // Si el jugador colisiona con el agua, lo reinicia en su punto de reaparición
        if (collision.gameObject.name == "water" || collision.gameObject.name == "Car(Clone)")
        {
            transform.position = spawnPoint;
            targetPosition = Vector3.zero;
            currentPosition = spawnPoint;
        }

        // Si el jugador está sobre una plataforma, lo asocia a ella
        if (collision.gameObject.CompareTag("Platform"))
        {
            inPlatform = true;
            currentPlatform = collision.gameObject;
            lastPlatformPosition = currentPlatform.transform.position; // Guarda la posición inicial de la plataforma
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Si el jugador deja de estar sobre la plataforma, lo desvincula de ella
        if (collision.gameObject.CompareTag("Platform"))
        {
            inPlatform = false;
            currentPlatform = null;
        }
    }
}
