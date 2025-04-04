using UnityEngine;

public class Personaje3D : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;
    public Animator anim;
    public Transform eje;

    public bool inground;
    public float distance = 0.2f;
    public Vector3 v3;

    public bool canMove = true;
    private bool isDead = false;

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Si no puede moverse o está muerto, desactivar "Run" y salir
        if (!canMove || isDead)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // Mantener gravedad
            anim.SetBool("Run", false);
            return;
        }

        // Calcular dirección de movimiento
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) direction += eje.forward;
        if (Input.GetKey(KeyCode.S)) direction -= eje.forward;
        if (Input.GetKey(KeyCode.D)) direction += eje.right;
        if (Input.GetKey(KeyCode.A)) direction -= eje.right;

        direction.y = 0; // Ignorar eje Y

        // Normalizar y calcular velocidad
        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
            float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * 2f : speed;
            rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
            
            // Rotación suave hacia la dirección de movimiento
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            
            anim.SetBool("Run", true); // Activar animación de correr
        }
        else
        {
            // Si no hay input, detener movimiento en X/Z pero mantener gravedad
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            anim.SetBool("Run", false); // Desactivar animación de correr
        }
    }

    void Update()
    {
        // Verificar si está en el suelo
        inground = Physics.Raycast(transform.position + v3, Vector3.down, distance);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + v3, Vector3.down * distance);
    }

    public void SetMovementLock(bool locked)
    {
        canMove = !locked;
        if (locked)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            anim.SetBool("Run", false);
        }
    }

    public void OnDeath()
    {
        isDead = true;
        rb.velocity = Vector3.zero;
        anim.SetBool("Run", false);
    }
}