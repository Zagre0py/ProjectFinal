using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public Animator anim;
    public Image healthBar;
    public Collider weaponCollider;

    [Header("Configuración")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float attackRange = 3f;
    public float detectionRange = 15f;
    public float timeBetweenAttacks = 3f;
    public float phase2Threshold = 0.6f; // 60% de vida
    public float phase3Threshold = 0.3f; // 30% de vida

    [Header("Vida")]
    public float maxHealth = 1000f;
    private float currentHealth;

    // Estados
    private bool isDead = false;
    private bool isAttacking = false;
    private float attackCooldown;
    private int currentPhase = 1;
    private int attackPattern = 0;

    void Start()
    {
        currentHealth = maxHealth;
        attackCooldown = timeBetweenAttacks;
        
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        UpdateHealth();
        CheckPhaseChange();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            FacePlayer();
            
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            else if (attackCooldown <= 0 && !isAttacking)
            {
                StartCoroutine(PerformAttack());
            }
        }

        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }

    void UpdateHealth()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void CheckPhaseChange()
    {
        float healthPercent = currentHealth / maxHealth;

        if (healthPercent <= phase3Threshold && currentPhase != 3)
        {
            currentPhase = 3;
            ChangeAttackPattern();
        }
        else if (healthPercent <= phase2Threshold && currentPhase != 2)
        {
            currentPhase = 2;
            ChangeAttackPattern();
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveTowardsPlayer()
    {
        if (isAttacking) return;

        Vector3 moveDirection = (player.position - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        anim.SetBool("IsMoving", true);
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;
        anim.SetBool("IsMoving", false);
        attackCooldown = timeBetweenAttacks;

        // Seleccionar ataque según fase y patrón
        string attackTrigger = "Attack" + currentPhase + "_" + (attackPattern % 3 + 1);
        anim.SetTrigger(attackTrigger);

        // Esperar durante el ataque (ajustar según duración de animación)
        yield return new WaitForSeconds(1.5f);

        isAttacking = false;
        attackPattern++;
    }

    void ChangeAttackPattern()
    {
        // Reducir tiempo entre ataques en fases avanzadas
        timeBetweenAttacks *= 0.7f;
        
        // Cambiar a nuevos ataques
        attackPattern = 0;
        Debug.Log("Cambiando a Fase " + currentPhase + " con nuevos ataques!");
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("Die");
        healthBar.gameObject.SetActive(false);
        Destroy(gameObject, 5f);
    }

    // Llamados desde Animation Events
    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }
}