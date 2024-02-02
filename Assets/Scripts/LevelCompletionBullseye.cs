using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletionBullseye : MonoBehaviour
{
    private void Start()
    {
        EnemyManager.Instance.OnAllEnemiesDead += CompleteLevel;
    }

    private void CompleteLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }
}
