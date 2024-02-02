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
        AudioManager.Instance.OnKick += Shoot;
    }

    private void Update()
    {
        if (inputManager.PlayerShot() && isGunOut)
        {
            //Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        cameraShake.GenerateImpulse(cameraShakeForce);

        if (Physics.Raycast(cameraMainTransform.position, cameraMainTransform.forward, out RaycastHit hitInfo, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            IShootable shootable = hitInfo.collider.GetComponentInParent<IShootable>();

            if (shootable != null)
            {
                shootable.OnShot(hitInfo.collider.gameObject, hitInfo.point, cameraMainTransform.forward);
            }
        }
    }

    public void GunOut()
    {
        isGunOut = true;
    }
}
