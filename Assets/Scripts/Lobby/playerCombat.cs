using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint; // Un empty GameObject en la mano del player
    public float attackRange = 1f; // Rango del golpe
    public int attackDamage = 1; // Daño que inflige
    public LayerMask enemyLayers; // Enemigos a los que afecta

    private bool isAttacking = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking) // Click izquierdo
        {
            StartCoroutine(Punch());
        }
    }

    IEnumerator Punch()
    {
        isAttacking = true;
        anim.SetBool("isPunching", true); // Activar animación de golpe
        yield return new WaitForSeconds(0.2f); // Esperar un poco para sincronizar el golpe

        // Detectar enemigos en el rango de ataque
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<enemyBase>()?.TakeDamage(attackDamage);
        }

        yield return new WaitForSeconds(0.3f); // Esperar hasta que termine la animación
        anim.SetBool("isPunching", false);
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
