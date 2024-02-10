using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullseyeLevelComplete : MonoBehaviour
{
    [SerializeField] private Animator RoomGateAnimator;

    private void Start()
    {
        EnemyManager.Instance.OnAllEnemiesDead += CompleteLevel;
    }

    private void CompleteLevel()
    {
        AudioManager.Instance.StopKicks();
        RoomGateAnimator.SetTrigger("Open Gate");
    }
}
