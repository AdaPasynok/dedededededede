using UnityEngine;

public class BullseyeEnemyGateOpenner : MonoBehaviour, IShootable
{
    [SerializeField] private Animator enemyGatesAnimator;

    public void OnGotShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction)
    {
        enemyGatesAnimator.enabled = true;
    }
}
