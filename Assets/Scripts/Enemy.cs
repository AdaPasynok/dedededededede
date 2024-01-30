using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class Enemy : MonoBehaviour, IShootable
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform foot1, foot2;
    [Space]
    [SerializeField] private float gunShotForce = 50f;
    [SerializeField] private ParticleSystem bloodSplash;
    [Space]
    [SerializeField] private Transform head;
    [SerializeField] private Transform playerHead;
    [SerializeField] private LayerMask ignoreSelfMask;

    private NavMeshAgent navMeshAgent;
    private RigBuilder rigBuilder;
    private Rigidbody[] rigidbodies;
    private float heightOffset;
    private bool isDead = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigBuilder = GetComponent<RigBuilder>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        heightOffset = transform.position.y - GetAverageFootHeight();
    }

    private void Update()
    {
        if (!isDead)
        {
            //Walk();

            Vector3 directionToPlayer = playerHead.position - head.position;

            if (Physics.Raycast(head.position, directionToPlayer, out RaycastHit hitInfo, Mathf.Infinity, ignoreSelfMask))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    ChasePlayer();
                }
            }
        }
    }

    private void ChasePlayer()
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(playerHead.position, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            navMeshAgent.path = path;
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
            navMeshAgent.isStopped = true;
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
