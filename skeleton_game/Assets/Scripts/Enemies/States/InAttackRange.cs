using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InAttackRange : State
{
    public override void LookForPlayer(Enemy context, Vector3 currentPosition, Vector3 belowPosition, NavMeshAgent agent,
                                        Transform player, LayerMask whatIsGround, Animator animator)
    {
        // Lost sight of player
        context.State = new Passive();
        Debug.Log("Changed state to: Passive");

        animator.SetBool("inAttackRange", false);
    }

    public override void ChasePlayer(Enemy context, NavMeshAgent agent, Transform player, Animator animator)
    {
        // Chasing player
        context.State = new InPursuit();
        Debug.Log("Changed state to: InPursuit");

        animator.SetBool("inAttackRange", false);
    }

    public override void AttackPlayer(Enemy context, NavMeshAgent agent, Transform player, Animator animator)
    {
        // Attacking Player

        agent.SetDestination(player.position);

        animator.SetBool("inAttackRange", true);

    }
}
