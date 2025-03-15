using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeEnemy : enemyBase
{
    public float hoverHeight = 2f; // Altura a la que vuela la abeja
    public float attackCooldown = 1.5f; // Tiempo entre ataques
    private float lastAttackTime;

    protected override void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                AttackPlayer();
            }
        }
        else if (distanceToPlayer <= detectionRange)
        {
            currentState = EnemyState.Run;
            FlyTowardsPlayer(); // Nueva función para moverse en el aire
        }
        else
        {
            currentState = EnemyState.Idle;
        }

        HandleStates();
    }

    private void FlyTowardsPlayer()
    {
        // Calcular la dirección hacia el jugador manteniendo una altura fija
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + hoverHeight, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(player);
    }
    private void AttackPlayer()
    {
        currentState = EnemyState.Attack;

        //Busca la vida en la escena
        healthManager healthManager = FindAnyObjectByType<healthManager>();

        if (healthManager != null)
        {
            healthManager.Hurt();
        }
        else
        {
            Debug.LogWarning("No se encontró HealthManager en la escena.");
        }
    }
}
