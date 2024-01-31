using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [Space]
    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private float timeBetweenShots = 0.5f;

    private float timer = 0f;

    protected override void OnPlayerDetected(Vector3 playerPosition)
    {
        if (timer <= 0f)
        {
            timer = timeBetweenShots;
            muzzleFlash.Play();
            GameObject bulletInstance = Instantiate(bullet, muzzleFlash.transform.position, Quaternion.identity);
            bulletInstance.GetComponent<Bullet>().direction = muzzleFlash.transform.forward;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
