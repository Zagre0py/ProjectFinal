using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
     public float moveSpeed = 5f; // Velocidad de movimiento
    public float jumpForce = 5f; // Fuerza de salto
    public float groundCheckDistance = 0.1f; // Distancia para verificar el suelo
    public LayerMask groundMask; // Capa que representa el suelo
    public float rotationSpeed = 10f; // Velocidad de rotación

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Verificar si el personaje está en el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        // Obtener la entrada del jugador
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Mover al personaje en la dirección deseada
        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized;
        Vector3 moveVelocity = move * moveSpeed;

        // Aplicar movimiento al Rigidbody
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

         // Rotar el personaje hacia la dirección del movimiento
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
