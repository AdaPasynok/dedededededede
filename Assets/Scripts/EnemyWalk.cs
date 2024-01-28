using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform foot1, foot2;

    private float heightOffset;

    private void Start()
    {
        heightOffset = transform.position.y - GetAverageFootHeight();
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * transform.forward, Space.World);

        Vector3 position = transform.position;
        position.y = GetAverageFootHeight() + heightOffset;
        transform.position = position;
    }

    private float GetAverageFootHeight()
    {
        return (foot1.position.y + foot2.position.y) / 2f;
    }
}
