using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchStatue : MonoBehaviour, IShootable
{
    [SerializeField] private float rotationTime = 0.25f;
    [SerializeField] private float watchTime = 1f;
    [SerializeField] private LayerMask wallMask;

    private GameObject watchedObject = null;
    private Vector3 oldRotation, newRotation;
    private float lerp = 0f;
    private float timeWatched = 0f;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, Mathf.Infinity, wallMask))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (hitObject == watchedObject)
            {
                if (timeWatched >= watchTime)
                {
                    Debug.Log(watchedObject);
                }
                else
                {
                    timeWatched += Time.deltaTime;
                }
            }
            else
            {
                watchedObject = hitObject;
                timeWatched = 0f;
            }
        }
    }

    public void OnShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction)
    {
        oldRotation = transform.eulerAngles;
        transform.forward = direction;
        newRotation = transform.eulerAngles;
        newRotation.x = 0f;
        float yDifference = newRotation.y - oldRotation.y;

        if (Mathf.Abs(yDifference) > 180f)
        {
            oldRotation.y += 360f * Mathf.Sign(yDifference);
        }

        transform.eulerAngles = oldRotation;
        lerp = 0f;
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (lerp < 1f)
        {
            lerp += Time.deltaTime * (1f / rotationTime);
            transform.eulerAngles = Vector3.Lerp(oldRotation, newRotation, lerp);
            yield return null;
        }
    }
}
