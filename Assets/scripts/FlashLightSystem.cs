using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDeclay = 1f;
    [SerializeField] float minimumAngle = 40f;
    [SerializeField] float minimumIntensity = 1f;

    Light myLight;
    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();

    }

    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
    }
    public void AddLightIntensity(float addIntensity) 
    {
        myLight.intensity += addIntensity;
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle > minimumAngle)
        {
            myLight.spotAngle -= angleDeclay * Time.deltaTime;
        }
        else
        {
            return;
        }
    }
    private void DecreaseLightIntensity()
    {
        if (myLight.intensity > minimumIntensity)
        {
            myLight.intensity -= lightDecay * Time.deltaTime;
        }
        else
        {
            return;
        }
         
        
    }
}
