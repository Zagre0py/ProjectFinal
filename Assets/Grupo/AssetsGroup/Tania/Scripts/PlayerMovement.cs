using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPosition;
    private Vector3 spawnPoint;
    private Vector3 currentPosition;
    private Vector3 lastPlatformPosition;

    public float moveDistance = 1f;
    public float moveSpeed = 5f;

    private bool isMoving = false;
    private bool inPlatform = false;
    private bool inFloor = false;

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

            if (Vector3.Distance(rb.position, currentPosition) < 0.01f)
            {
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
