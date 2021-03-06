using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float hitPoints = 100f;
    [SerializeField] GameObject Sphere;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
        CapsuleCollider capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        Destroy(capsuleCollider);
        Destroy(Sphere);
    }
}
