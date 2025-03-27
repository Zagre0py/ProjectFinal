using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPosition;
    private Vector3 spawnPoint;
    private Vector3 currentPosition;
    private Vector3 lastPlatformPosition;

    public float moveDistance = 1f;
    public float moveSpeed = 5f;

    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool inPlatform = false;
    [SerializeField] private bool inFloor = false;
    [SerializeField] private bool onLimit = false;



    private GameObject currentPlatform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
        spawnPoint = transform.position;
        currentPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving && (inPlatform || inFloor))
        {
            if (Input.GetKeyDown(KeyCode.W)) Move(Vector3.forward);
            if (Input.GetKeyDown(KeyCode.S)) Move(Vector3.back);
            if (Input.GetKeyDown(KeyCode.A)) Move(Vector3.left);
            if (Input.GetKeyDown(KeyCode.D)) Move(Vector3.right);
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, currentPosition, moveSpeed * Time.fixedDeltaTime));

            // Si está lo suficientemente cerca, ajusta la posición exacta y detiene el movimiento
            if (Vector3.Distance(rb.position, currentPosition) < 0.05f)
            {
                rb.position = currentPosition; // Fija la posición exacta
                isMoving = false;
            }
        }

        if (inPlatform && currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.transform.position - lastPlatformPosition;
            currentPosition += platformMovement;
            rb.MovePosition(currentPosition);
            lastPlatformPosition = currentPlatform.transform.position;
        }
    }

    void Move(Vector3 direction)
    {
        if (isMoving) return; // Evita iniciar un nuevo movimiento mientras el jugador se está moviendo

        currentPosition = rb.position + direction * moveDistance;
        isMoving = true;

        // Si se mueve, se desvincula de la plataforma
        inPlatform = false;
        currentPlatform = null;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            inFloor = true;
        }
        if (collision.gameObject.CompareTag("Limit"))
        {
            isMoving = false;

            // Asegurar que el Rigidbody no sea kinematic
            if (rb.isKinematic) return;

            // Obtener la dirección opuesta al contacto
            Vector3 pushDirection = (transform.position - collision.contacts[0].point).normalized;

            // Aplicar fuerza en la dirección opuesta
            float pushForce = 30f; // Aumenta el valor si el empuje es muy débil
            rb.velocity = Vector3.zero; // Resetear la velocidad antes de aplicar la fuerza
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            transform.position = spawnPoint;
            targetPosition = Vector3.zero;
            currentPosition = spawnPoint;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            inPlatform = true;
            currentPlatform = collision.gameObject;
            lastPlatformPosition = currentPlatform.transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            inPlatform = false;
            currentPlatform = null;
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            inFloor = false;
        }

    }
}
