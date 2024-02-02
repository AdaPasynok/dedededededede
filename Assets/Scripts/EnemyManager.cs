using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public event Action OnAllEnemiesDead;

    private int enemyCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void OnEnemySpawn()
    {
        enemyCount++;
    }

    public void OnEnemyKilled()
    {
        enemyCount--;

        if (enemyCount == 0)
        {
            OnAllEnemiesDead?.Invoke();
        }
    }
}
