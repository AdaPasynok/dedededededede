using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolTrigger : MonoBehaviour
{
    [SerializeField] private Animator pistolAnimator, crosshairAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pistolAnimator.SetTrigger("Pull Out");
            crosshairAnimator.SetTrigger("Fade In");
            gameObject.SetActive(false);
        }
    }
}
