using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 700f;
    public Transform cameraTarget; // Drag the camera here

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents weird rotations
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float moveDirection = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveDirection * speed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    void Rotate()
    {
        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * turn * rotationSpeed * Time.deltaTime);
    }
}