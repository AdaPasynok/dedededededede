using UnityEngine;

public interface IShootable
{
    public void OnGotShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction);
}
