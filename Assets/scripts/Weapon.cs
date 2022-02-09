using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem bulletOut;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] int onSoot = 2;

    public GameObject metalHitEffect;
    public GameObject sandHitEffect;
    public GameObject stoneHitEffect;
    public GameObject[] fleshHitEffects;
    public GameObject woodHitEffect;
    

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }
    


    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButton(0) && canShoot == true) //Input.GetMouseButtonDown(0);
        {
            GetComponent<Animator>().SetBool("stop", true);
            StartCoroutine(Shoot());
        }
        else
        {
            GetComponent<Animator>().SetBool("stop", false);
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ShootGun();
            PlayMuzzleFlash();
            PlayBulletOut();
            ProcessRecast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
        

    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }
    private void PlayBulletOut()
    {
        bulletOut.Play();
    }
    private void ShootGun()
    {
        GetComponent<Animator>().SetTrigger("Fire");
        
    }

    private void ProcessRecast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            //ShootGun();
            HandleHit(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }
    private void HandleHit(RaycastHit hit)
    {
        if (hit.collider.sharedMaterial != null)
        {
            string materialName = hit.collider.sharedMaterial.name;

            switch (materialName)
            {
                case "Metal":
                    SpawnDecal(hit, metalHitEffect);
                    break;
                case "Sand":
                    SpawnDecal(hit, sandHitEffect);
                    break;
                case "Stone":
                    SpawnDecal(hit, stoneHitEffect);
                    break;
                case "Wood":
                    SpawnDecal(hit, woodHitEffect);
                    break;
                case "Meat":
                    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
                case "Character":
                    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
            }
        }
        
    }
    private void SpawnDecal(RaycastHit hit, GameObject prefab)
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnedDecal.transform.SetParent(hit.collider.transform);
        Destroy(spawnedDecal, 0.4f);
    }
}
