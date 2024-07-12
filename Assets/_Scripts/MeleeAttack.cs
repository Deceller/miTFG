using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 1.5f;  // Distancia del ataque
    public float attackAngle = 90f;   // Ángulo del ataque
    public int damage = 10;           // Daño del ataque
    public float attackCooldown = 0.7f; // Tiempo de enfriamiento entre ataques
    private Animator _animatorController;

    public bool isEnemy = true;
    private bool canAttack = true;


    private void Awake()
    {
        _animatorController = GetComponent<Animator>();
    }

    void Update()
    {
        if (isEnemy)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(PerformMeleeAttack());
            _animatorController.SetBool("AttackingP", true);
        }
    }

    public IEnumerator PerformMeleeAttack()
    {
        canAttack = false;

        // Obtener todos los objetos en el rango del ataque
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var hitCollider in hitColliders)
        {

            if (hitCollider.gameObject != gameObject) // Ignorar al propio personaje
            {
                Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;
                float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

                // Verificar si el objetivo está dentro del ángulo de ataque
                if (angleToTarget < attackAngle / 2)
                {
                    // Aplicar daño al objetivo si tiene un componente de salud
                    Health targetHealth = hitCollider.GetComponent<Health>();
                    if (targetHealth != null)
                    {
                        targetHealth.TakeDamage(damage);
                    }
                }
            }
        }

        // Esperar el tiempo de enfriamiento antes de permitir otro ataque
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        _animatorController.SetBool("AttackingP", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Vector3 rightLimit = Quaternion.Euler(0, attackAngle / 2, 0) * transform.forward * attackRange;
        Vector3 leftLimit = Quaternion.Euler(0, -attackAngle / 2, 0) * transform.forward * attackRange;
        Gizmos.DrawLine(transform.position, transform.position + rightLimit);
        Gizmos.DrawLine(transform.position, transform.position + leftLimit);
    }
}