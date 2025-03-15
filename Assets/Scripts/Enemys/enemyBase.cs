using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBase : MonoBehaviour
{
    public int health = 3;
    public float detectionRange = 7f;
    public float attackRange = 3f;
    public int damage = 1;
    public float speed = 3f;
    private Vector3 initialPosition; //Guarda la posicion inicial
    private Quaternion initialRotation; // Guarda la rotación inicial

    protected Transform player;
    protected Animator anim;
    protected NavMeshAgent agent;

    protected enum EnemyState { Idle, Run, Attack }
    protected EnemyState currentState = EnemyState.Idle;
    protected virtual void Start()
    {
        initialPosition = transform.position; // Guarda la posición
        initialRotation = transform.rotation; // Guarda la rotación
        healthManager.OnPlayerDeath += ResetEnemyPosition; // Escucha la muerte del jugador
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }
    protected virtual void Update()
    {
        if (player == null || health <= 0) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            ChangeState(EnemyState.Attack);
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChangeState(EnemyState.Run);
        }
        else
        {
            ChangeState(EnemyState.Idle);
        }
    }
    protected void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        HandleStates();
    }
    protected virtual void HandleStates()
    {
        if (anim == null) return; // Evita errores si el Animator no está asignado
        switch (currentState)
        {
            case EnemyState.Idle:
                anim.SetBool("isRunning", false);
                anim.SetBool("isAttacking", false);
                agent.isStopped = true;
                break;

            case EnemyState.Run:
                anim.SetBool("isRunning", true);
                anim.SetBool("isAttacking", false);
                agent.isStopped = false;
                agent.SetDestination(player.position);
                break;

            case EnemyState.Attack:
                if (!anim.GetBool("isAttacking")) // Solo activar si no está atacando ya
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isAttacking", true);
                    agent.isStopped = true;
                    StartCoroutine(AttackCooldown());
                }
                break;
        }
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(3.5f);
        ChangeState(EnemyState.Idle);
    }
    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log(gameObject.name + " recibió " + damageAmount + " de daño. Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        anim.SetTrigger("Die");
        GetComponent<Collider>().enabled = false; // Desactivar colisión
        agent.enabled = false; // Desactivar navegación
        gameObject.SetActive(false); // Desactivar enemigo
    }
    private void ResetEnemyPosition()
    {
        transform.position = initialPosition; // Restaurar posición
        transform.rotation = initialRotation; // Restaurar rotación
        gameObject.SetActive(true); // Asegurar que el enemigo esté activo
        health = 3; // Restaurar salud
        GetComponent<Collider>().enabled = true;
        agent.enabled = true;
        agent.isStopped = false;
        currentState = EnemyState.Idle; // Reiniciar estado
    }
    private void OnDestroy()
    {
        healthManager.OnPlayerDeath -= ResetEnemyPosition; // Evitar referencias fantasma
    }
}
