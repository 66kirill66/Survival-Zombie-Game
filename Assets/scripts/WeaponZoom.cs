using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 0.5f;
    [SerializeField] Canvas GunReticle;




    bool zoomedInToggle = false;

    private void OnDisable()
    {
        //ZoomOut();
        //ZoomIn();
    }
   


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();

            }
        }
    }

    private void ZoomOut()
    {
        GunReticle.enabled = true;
        zoomedInToggle = false;
        GetComponent<Animator>().SetBool("ZoomIn2", false);
        GetComponent<Animator>().SetTrigger("ZoomOut");
        //fpsCamera.fieldOfView = zoomedOutFOV;
        fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
    }

    private void ZoomIn()
    {
        GunReticle.enabled = false;
        zoomedInToggle = true;
        GetComponent<Animator>().SetBool("Fire", false);
        GetComponent<Animator>().SetBool("ZoomIn2", true);
        //GetComponent<Animator>().SetTrigger("ZoomIn");
        //fpsCamera.fieldOfView = zoomedInFOV;
        fpsController.mouseLook.XSensitivity = zoomInSensitivity;
        fpsController.mouseLook.YSensitivity = zoomInSensitivity;
    }
}
