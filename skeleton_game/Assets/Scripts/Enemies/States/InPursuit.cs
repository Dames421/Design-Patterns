using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InPursuit : State
{
    public override void LookForPlayer(Enemy context, Vector3 currentPosition, Vector3 belowPosition, NavMeshAgent agent,
                                        Transform player, LayerMask whatIsGround, Animator animator)
    {
        // Lost sight of player
        context.State = new Passive();
        Debug.Log("Changed state to: Passive");
    }

    public override void ChasePlayer(Enemy context, NavMeshAgent agent, Transform player, Animator animator)
    {
        // Player found. Chasing player
        agent.SetDestination(player.position);
    }

    public override void AttackPlayer(Enemy context, NavMeshAgent agent, Transform player, Animator animator)
    {
        // Attack player
        context.State = new InAttackRange();
        Debug.Log("Changed state to: InAttackRange");
    }
}
