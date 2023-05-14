using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Variables
    public NavMeshAgent agent;
    public Transform player;
    public Vector3 currentPosition;
    public Vector3 belowPosition;
    public LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] Animator animator;
    [SerializeField] Enemy anEnemy;
    
    public static float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // The skeletons in this game don't take damage, so these variables are purely for demonstrative purposes.
    public static int maxHealth = 30;
    public static int maxSpeed = 12;
    public float currentHealth;
    

    // Methods
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        currentPosition = transform.position;
        belowPosition = transform.up;

        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            anEnemy.LookForPlayer(currentPosition, belowPosition, agent, player, whatIsGround, animator);
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            anEnemy.ChasePlayer(agent, player, animator);
        }
        if (playerInSightRange && playerInAttackRange)
        {
            anEnemy.AttackPlayer(agent, player, animator);
        }

    }

}
