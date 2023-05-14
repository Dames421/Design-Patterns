using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Passive : State
{
    public Vector3 walkPoint = Vector3.zero;
    bool walkPointSet = false;
    public float walkPointRange = 5;

    public override void LookForPlayer(Enemy context, Vector3 currentPosition, Vector3 belowPosition, NavMeshAgent agent,
                                        Transform player, LayerMask whatIsGround, Animator animator)
    {
        if (!walkPointSet)
        {
            SearchWalkPoint(currentPosition, belowPosition, whatIsGround);
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = currentPosition - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint(Vector3 currentPosition, Vector3 belowPosition, LayerMask whatIsGround)
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(currentPosition.x + randomX, currentPosition.y, currentPosition.z + randomZ);

        if (Physics.Raycast(walkPoint, -belowPosition, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    public override void ChasePlayer(Enemy context, NavMeshAgent agent, Transform player, Animator animator)
    {
        // Player found. Chasing Player 
        context.State = new InPursuit();
        Debug.Log("Changed state to: InPursuit");


    }

    public override void AttackPlayer(Enemy context, NavMeshAgent agent, Transform player, Animator animator)
    {
        // Player not found
    }
    
}
