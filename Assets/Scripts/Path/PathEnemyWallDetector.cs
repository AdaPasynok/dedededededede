using UnityEngine;

public class PathEnemyWallDetector : MonoBehaviour
{
    private Renderer[] renderers;
    private Enemy thisEnemy;

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        thisEnemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            ToggleEnableState(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            ToggleEnableState(true);
        }
    }

    private void ToggleEnableState(bool state)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = state;
        }

        thisEnemy.ableToKill = state;
    }
}
