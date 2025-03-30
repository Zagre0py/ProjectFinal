using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje3D : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public Animator anim;
    public Transform eje;

    public bool inground;
    private RaycastHit hit;
    public float distance;
    public Vector3 v3;


    private void FixedUpdate() {
        Move();
    }
    void Move()
{
    Vector3 direction = Vector3.zero;
    
    if (Input.GetKey(KeyCode.W)) direction += eje.transform.forward;
    if (Input.GetKey(KeyCode.S)) direction -= eje.transform.forward;
    if (Input.GetKey(KeyCode.D)) direction += eje.transform.right;
    if (Input.GetKey(KeyCode.A)) direction -= eje.transform.right;

    direction.y = 0; // Asegura que la dirección es solo en el plano XZ

    bool isMoving = direction != Vector3.zero;
    
    if (isMoving)
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * 1.5f : speed; // Sprint con Shift
        MoveCharacter(direction.normalized, moveSpeed);
    }
    else if (inground)
    {
        rb.velocity = Vector3.zero;
    }

    anim.SetBool("Run", isMoving);
}

void MoveCharacter(Vector3 direction, float moveSpeed)
{
    float rotationSpeed = 10f; // Controla la suavidad de la rotación
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

    Vector3 velocity = direction * moveSpeed;
    velocity.y = rb.velocity.y; // Mantiene la velocidad vertical
    rb.velocity = velocity;
}

    void Update()
    {
        if(Physics.Raycast(transform.position + v3, transform.up *-1, out hit, distance)){

            if(hit.collider.tag == "piso"){
                inground = true;
            }
        }
        else{

            inground = false;
        }
    }

    void ODrawGizmos()
    {
        Gizmos.DrawRay(transform.position + v3, Vector3.up *-1* distance);
    }



}
