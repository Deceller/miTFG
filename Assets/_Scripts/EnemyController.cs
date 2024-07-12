using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;            // Referencia al jugador
    public float attackRange = 1.5f;    // Distancia del ataque
    public float attackCooldown = 1f;   // Tiempo de enfriamiento entre ataques

    private NavMeshAgent navMeshAgent;  // Componente de navegación
    private MeleeAttack meleeAttack;    // Componente de ataque melee
    private bool canAttack = true;      // Bandera para controlar el ataque
    private Animator _animatorController;
    private bool isAttacking = false;


    private void Awake()
    {
        _animatorController = GetComponent<Animator>();
    }

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        meleeAttack = GetComponent<MeleeAttack>();

        // Verificar si los componentes están presentes
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent no está adjunto al objeto " + gameObject.name);
        }

        if (meleeAttack == null)
        {
            Debug.LogError("MeleeAttack no está adjunto al objeto " + gameObject.name);
        }
    }

    void Update()
    {
        if (navMeshAgent != null && player != null)
        {
            if (isAttacking == false)
            {
                navMeshAgent.SetDestination(player.position);
            }
            // Moverse hacia el jugador
            

            // Verificar si el enemigo está dentro del rango de ataque
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && canAttack)
            {
                
                StartCoroutine(PerformMeleeAttack());
            }
        }
    }

    IEnumerator PerformMeleeAttack()
    {
        isAttacking = true;
        _animatorController.SetBool("Attacking", true);
        canAttack = false;
        yield return StartCoroutine(meleeAttack.PerformMeleeAttack());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; 
        _animatorController.SetBool("Attacking", false);
        isAttacking = false;
    }
}