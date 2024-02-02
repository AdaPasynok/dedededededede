using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyShooter : Enemy
{
    [Space]
    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem muzzleFlash;

    private MultiAimConstraint[] aimingRig;
    private GameManager gameManager;
    private bool isPlayerVisible = false;

    protected override void Start()
    {
        base.Start();
        OnPlayerDetected += (playerPosition) => isPlayerVisible = true;
        OnPlayerLost += () => isPlayerVisible = false;
        aimingRig = GetComponentsInChildren<MultiAimConstraint>();
        gameManager = GameManager.Instance;

        foreach (MultiAimConstraint constraint in aimingRig)
        {
            WeightedTransformArray sourceObjects = constraint.data.sourceObjects;
            sourceObjects.SetTransform(0, gameManager.playerBody);
            constraint.data.sourceObjects = sourceObjects;
        }

        rigBuilder.Build();
    }

    protected void Shoot()
    {
        if (!isDead && isPlayerVisible)
        {
            muzzleFlash.Play();
            GameObject bulletInstance = Instantiate(bullet, muzzleFlash.transform.position, Quaternion.identity);
            bulletInstance.GetComponent<Bullet>().direction = muzzleFlash.transform.forward;
        }
    }
}
