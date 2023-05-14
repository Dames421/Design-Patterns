using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Variables
    private State state;

    // Get and Set
    public State State
    {
        get { return this.state; }

        set { this.state = value; }
    }

    // Constructor
    public Enemy()
    {
        this.State = new Passive();
    }

    // Methods
    public void LookForPlayer(Vector3 currentPosition, Vector3 belowPosition, NavMeshAgent agent,
                                Transform player, LayerMask whatIsGround, Animator animator)
    {
        this.State.LookForPlayer(this, currentPosition, belowPosition, agent, player, whatIsGround, animator);
    }

    public void ChasePlayer(NavMeshAgent agent, Transform player, Animator animator)
    {
        this.State.ChasePlayer(this, agent, player, animator);
    }

    public void AttackPlayer(NavMeshAgent agent, Transform player, Animator animator)
    {
        this.State.AttackPlayer(this, agent, player, animator);
    }
}
