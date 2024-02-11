using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public abstract class Enemy : MonoBehaviour, IShootable
{
    public bool ableToKill = true;

    [SerializeField] protected bool isActive = true;
    [Space]
    [SerializeField] private Transform head;
    [SerializeField] private LayerMask ignoreSelfMask;
    [Space]
    [SerializeField] private float gunShotForce = 50f;
    [SerializeField] private ParticleSystem bloodSplash;

    protected event Action<Vector3> OnPlayerDetected;
    protected event Action OnPlayerLost;

    protected RigBuilder rigBuilder;
    protected bool isDead = false;

    private EnemyManager enemyManager;
    private Collider outerTriggerColliderForPlayerCollisionDetection;
    private Transform playerHead;
    private Rigidbody[] rigidbodies;

    protected virtual void Start()
    {
        enemyManager = EnemyManager.Instance;
        enemyManager.OnEnemySpawn();
        outerTriggerColliderForPlayerCollisionDetection = GetComponent<Collider>();
        playerHead = GameManager.Instance.playerHead;
        rigBuilder = GetComponent<RigBuilder>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            Vector3 directionToPlayer = playerHead.position - head.position;

            if (Physics.Raycast(head.position, directionToPlayer, out RaycastHit hitInfo, Mathf.Infinity, ignoreSelfMask))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    OnPlayerDetected?.Invoke(playerHead.position);
                }
                else
                {
                    OnPlayerLost?.Invoke();
                }
            }
        }
    }

    public void OnGotShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction)
    {
        if (!isDead)
        {
            Die();
            objectShot.GetComponent<Rigidbody>().AddForceAtPosition(gunShotForce * direction, hitPoint, ForceMode.Impulse);
            bloodSplash.transform.SetParent(objectShot.transform);
            bloodSplash.transform.position = hitPoint;
            bloodSplash.transform.forward = -direction;
            bloodSplash.Play();
        }
    }

    public virtual void Die()
    {
        if (!isDead)
        {
            isDead = true;
            enemyManager.OnEnemyKilled();
            outerTriggerColliderForPlayerCollisionDetection.enabled = false;
            rigBuilder.enabled = false;

            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }
    }
}
