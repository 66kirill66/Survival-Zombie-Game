using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;


    NavMeshAgent navMeshAngent;
    float distansToTarget = Mathf.Infinity;  // означает, что это гигантское число

    bool isProvoked = false;  // что вас не провоцируют. 

    EnemyHealth health;
    Transform target;


    void Start()
    {
        navMeshAngent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    void Update()
    {
        if (health.IsDead())
        {
            navMeshAngent.isStopped = true;
            enabled = false;          
        }
        distansToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distansToTarget <= chaseRange)
        {
            isProvoked = true;
        }
       

    }
    public void OnDamageTaken()
    {
        isProvoked = true;
    }


    private void EngageTarget()
    {
        FaceTarget();
        if (distansToTarget >= navMeshAngent.stoppingDistance)
        {
            ChaseTarget();
        }
        
        if (distansToTarget <= navMeshAngent.stoppingDistance)
        {
            AttackTarget();
        }
        
        
    }

    private void ChaseTarget()    // chasing = погоня   
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAngent.SetDestination(target.position);  
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion LookRotarion = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, LookRotarion, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
   
}
