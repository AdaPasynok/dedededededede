using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform cameraMainTransform;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private float cameraShakeForce = 1f;

    private InputManager inputManager;
    private CinemachineImpulseSource cameraShake;
    private bool isGunOut = false;

    private void Start()
    {
        inputManager = InputManager.Instance;
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (inputManager.PlayerShot() && isGunOut)
        {
            muzzleFlash.Play();
            cameraShake.GenerateImpulse(cameraShakeForce);

            if (Physics.Raycast(cameraMainTransform.position, cameraMainTransform.forward, out RaycastHit hitInfo, Mathf.Infinity, layerMask))
            {
                Debug.Log(hitInfo.collider.name);
            }
        }
    }

    public void GunOut()
    {
        isGunOut = true;
    }
}
