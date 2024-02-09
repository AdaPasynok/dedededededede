using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEnemiesFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject enemies;

    private bool isFollowing = false;

    private void Start()
    {
        AudioManager.Instance.OnKick += ToggleFollowness;
    }

    private void LateUpdate()
    {
        if (isFollowing)
        {
            Vector3 newEnemiesPosition = transform.position;
            newEnemiesPosition.y = enemies.transform.position.y;
            enemies.transform.position = newEnemiesPosition;
        }
    }

    private void ToggleFollowness()
    {
        isFollowing = !isFollowing;

        if (isFollowing)
        {
            Vector3 newEnemiesRotation = enemies.transform.eulerAngles;
            newEnemiesRotation.y = Random.Range(0f, 360f);
            enemies.transform.eulerAngles = newEnemiesRotation;
        }
    }
}
