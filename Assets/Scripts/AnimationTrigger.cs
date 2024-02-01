using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator[] animators;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Animator animator in animators)
            {
                animator.enabled = true;
            }

            gameObject.SetActive(false);
        }
    }
}
