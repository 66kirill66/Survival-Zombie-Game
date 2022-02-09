using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAmgle = 90f;
    [SerializeField] int addIntensity = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<FlashLightSystem>().RestoreLightAngle(restoreAmgle);
            other.GetComponentInChildren<FlashLightSystem>().AddLightIntensity(addIntensity);
            Destroy(gameObject);

        }
    }




}

