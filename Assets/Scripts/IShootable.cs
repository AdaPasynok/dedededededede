using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    public void OnShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction);
}
