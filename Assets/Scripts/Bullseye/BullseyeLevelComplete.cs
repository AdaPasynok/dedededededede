using UnityEngine;

public class BullseyeLevelComplete : MonoBehaviour
{
    [SerializeField] private Animator RoomGateAnimator;

    private GameManager gameManager;

    private void Start()
    {
        EnemyManager.Instance.OnAllEnemiesDead += CompleteLevel;
        gameManager = GameManager.Instance;
    }

    private void CompleteLevel()
    {
        AudioManager.Instance.StopKicks();
        RoomGateAnimator.SetTrigger("Open Gate");
        gameManager.FadeInExitNoise();
        gameManager.PutPlayerGunAway();
    }
}
