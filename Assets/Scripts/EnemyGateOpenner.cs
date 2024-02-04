using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGateOpenner : MonoBehaviour, IShootable
{
    [SerializeField] private Animator enemyGatesAnimator;

    public void OnGotShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction)
    {
        enemyGatesAnimator.enabled = true;
    }
}
