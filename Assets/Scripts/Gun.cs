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
    [SerializeField] private Animator noLMBAnimator;

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
            noLMBAnimator.SetTrigger("LMB Pressed");
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
                shootable.OnGotShot(hitInfo.collider.gameObject, hitInfo.point, cameraMainTransform.forward);
            }
        }
    }

    public void GunOut()
    {
        isGunOut = true;
    }
}
