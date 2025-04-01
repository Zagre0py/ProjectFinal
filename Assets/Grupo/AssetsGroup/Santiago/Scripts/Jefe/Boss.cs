using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public Animator anim;
    
    public Collider normalAttackCollider;
    public Collider jumpAttackCollider;
    private PlayerHealth playerHealth;

    [Header("Configuración")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float attackRange = 2f;
    public float detectionRange = 15f;
    public float timeBetweenAttacks = 3f;
    public float phaseThreshold = 0.5f; // Cambio de fase al 50% de vida

    [Header("Vida")]
    public int vida;
    public Slider vidaVisual;


    // Estados
    private bool isDead = false;
    private bool isAttacking = false;
    private float attackCooldown;
    private bool isPhase2 = false;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        
       
        
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        HandleMovement();
        HandleAttacks();
        VidaJefe();
    }

    void HandleMovement()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange && !isAttacking)
        {
            // Rotar para mirar al jugador
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Moverse hacia el jugador si está fuera de rango de ataque
            if (distanceToPlayer > attackRange)
            {
                transform.position += direction * moveSpeed * Time.deltaTime;
                anim.SetBool("IsMoving", true);
            }
            else
            {
                anim.SetBool("IsMoving", false);
            }
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    void HandleAttacks()
    {
        if (isAttacking) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= attackRange && attackCooldown <= 0)
        {
            StartCoroutine(PerformAttack());
        }

        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;
        attackCooldown = timeBetweenAttacks;

        // Seleccionar ataque según fase
        if (!isPhase2 || Random.value > 0.5f) // 50% de probabilidad en fase 2
        {
            // Ataque normal
            anim.SetTrigger("Attack_Normal");
            normalAttackCollider.enabled = true;
            yield return new WaitForSeconds(1f); // Duración del ataque normal
            normalAttackCollider.enabled = false;
        }
        else
        {
            // Ataque de salto
            anim.SetTrigger("Attack_Jump");
            jumpAttackCollider.enabled = true;
            yield return new WaitForSeconds(1.8f); // Duración del ataque de salto
            jumpAttackCollider.enabled = false;
        }

        isAttacking = false;
    }


    void EnterPhase2()
    {
        isPhase2 = true;
        timeBetweenAttacks *= 0.7f; // Ataques más frecuentes
        moveSpeed *= 1.2f; // Más rápido
        anim.SetTrigger("PhaseChange");
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("Die");
        normalAttackCollider.enabled = false;
        jumpAttackCollider.enabled = false;
        this.enabled = false;
    }

   public void VidaJefe(){

    vidaVisual.GetComponent<Slider>().value = vida;

    if(vida <= 0){

        Die();
    }
   }

    // Llamar desde Animation Events
    public void EnableNormalAttack() => normalAttackCollider.enabled = true;
    public void DisableNormalAttack() => normalAttackCollider.enabled = false;
    public void EnableJumpAttack() => jumpAttackCollider.enabled = true;
    public void DisableJumpAttack() => jumpAttackCollider.enabled = false;

    
}