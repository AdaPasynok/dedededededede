using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    private void Die()
    {
        GameManager.Instance.RestartLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>().ableToKill)
            {
                Die();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Die();
        }
    }
}
