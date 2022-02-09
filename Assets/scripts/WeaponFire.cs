using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        
    }

    public void FireEfect()
    {
        anim.SetTrigger("Fire");
    }
    
    
}
