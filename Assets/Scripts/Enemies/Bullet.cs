using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;

    [SerializeField] private float speed = 10f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
