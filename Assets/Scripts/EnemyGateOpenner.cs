using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGateOpenner : MonoBehaviour, IShootable
{
    [SerializeField] private Animator enemyGatesAnimator;

    public void OnShot(GameObject objectShot, Vector3 hitPoint, Vector3 directon)
    {
        enemyGatesAnimator.enabled = true;
    }
}
