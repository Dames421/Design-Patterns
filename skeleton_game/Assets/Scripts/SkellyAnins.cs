using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyAnins : MonoBehaviour
{
    // Variables
    [SerializeField] Animator animator;

    public float speed = 0;
    Vector3 lastPosition;

    // Methods

    private void Awake()
    {
        lastPosition = transform.position;
    }
    protected void FixedUpdate()
    {
        
        CallWalkAnim();
    }

    protected void CallWalkAnim()
    { 
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;

        animator.SetFloat("Speed", speed);

    }

    // Accessors
}
