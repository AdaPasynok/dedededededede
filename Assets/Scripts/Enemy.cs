using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Enemy : MonoBehaviour, IShootable
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform foot1, foot2;
    [SerializeField] private float gunShotForce = 50f;
    [SerializeField] private ParticleSystem bloodSplash;

    private RigBuilder rigBuilder;
    private Rigidbody[] rigidbodies;
    private float heightOffset;
    private bool isDead = false;

    private void Start()
    {
        rigBuilder = GetComponent<RigBuilder>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        heightOffset = transform.position.y - GetAverageFootHeight();
    }

    private void Update()
    {
        if (!isDead)
        {
            Walk();
        }
    }

    private void Walk()
    {
        transform.Translate(Time.deltaTime * speed * transform.forward, Space.World);

        Vector3 position = transform.position;
        position.y = GetAverageFootHeight() + heightOffset;
        transform.position = position;
    }

    private float GetAverageFootHeight()
    {
        return (foot1.position.y + foot2.position.y) / 2f;
    }

    private void EnableRagdoll()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    public void OnShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction)
    {
        if (!isDead)
        {
            isDead = true;
            rigBuilder.enabled = false;
            EnableRagdoll();
            objectShot.GetComponent<Rigidbody>().AddForceAtPosition(gunShotForce * direction, hitPoint, ForceMode.Impulse);

            bloodSplash.transform.SetParent(objectShot.transform);
            bloodSplash.transform.position = hitPoint;
            bloodSplash.transform.forward = -direction;
            bloodSplash.Play();
        }
    }
}
