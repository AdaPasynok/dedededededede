using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotatedEnemy : MonoBehaviour, IShootable
{
    private NoRotator parentRotator;
    private ShaderInverter shaderInverter;

    private void Start()
    {
        parentRotator = GetComponentInParent<NoRotator>();
        shaderInverter = GetComponent<ShaderInverter>();
    }

    public void OnGotShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction)
    {
        parentRotator.OnChildEnemyGotShot(shaderInverter);
    }
}
