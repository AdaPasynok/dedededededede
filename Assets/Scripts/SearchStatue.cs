using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SearchStatue : MonoBehaviour, IShootable
{
    [SerializeField] private float rotationTime = 0.25f;
    [SerializeField] private float watchTime = 1f;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private MultiAimConstraint statuePointAim;
    [SerializeField] private Animator roomGateAnimator;

    private WeightedTransformArray aimTargets;
    private GameObject watchedObject = null, lastWatchedObject = null;
    private bool isActive = true;
    private float timeWatched = 0f;
    private int currentIndex = 0;

    private void Start()
    {
        aimTargets = statuePointAim.data.sourceObjects;
    }

    private void Update()
    {
        if (isActive && Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, Mathf.Infinity, wallMask))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (hitObject == watchedObject)
            {
                if (timeWatched >= watchTime)
                {
                    if (watchedObject == aimTargets.GetTransform(currentIndex).gameObject)
                    {
                        currentIndex++;

                        if (currentIndex < aimTargets.Count)
                        {
                            lastWatchedObject = watchedObject;
                            StartCoroutine(Point(currentIndex - 1));
                        }
                        else
                        {
                            roomGateAnimator.SetTrigger("Open Gate");
                            AudioManager.Instance.StopKicks();
                            isActive = false;
                        }
                    }
                    else if (watchedObject != lastWatchedObject)
                    {
                        int previousIndex = currentIndex;
                        currentIndex = 0;
                        StartCoroutine(Point(previousIndex));
                        lastWatchedObject = watchedObject;
                    }
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

    public void OnGotShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction)
    {
        Vector3 oldRotation, newRotation;

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
        StartCoroutine(Rotate(oldRotation, newRotation));
    }

    private IEnumerator Rotate(Vector3 oldRotation, Vector3 newRotation)
    {
        float lerp = 0f;

        while (lerp < 1f)
        {
            lerp += Time.deltaTime * (1f / rotationTime);
            transform.eulerAngles = Vector3.Lerp(oldRotation, newRotation, lerp);
            yield return null;
        }
    }

    private IEnumerator Point(int previousIndex)
    {
        if (previousIndex != currentIndex)
        {
            float lerp = 0f;

            while (lerp < 1f)
            {
                lerp += Time.deltaTime * (1f / rotationTime);
                aimTargets.SetWeight(previousIndex, 1f - lerp);
                aimTargets.SetWeight(currentIndex, lerp);
                statuePointAim.data.sourceObjects = aimTargets;
                yield return null;
            }
        }
    }
}
