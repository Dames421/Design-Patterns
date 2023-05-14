using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    public abstract void LookForPlayer(Enemy context, Vector3 currentPosition, Vector3 belowPosition, NavMeshAgent agent,
                                        Transform player, LayerMask whatIsGround, Animator animator);

    public abstract void ChasePlayer(Enemy context, NavMeshAgent agent, Transform player, Animator animator);

    public abstract void AttackPlayer(Enemy context, NavMeshAgent agent, Transform player, Animator animator);

}
