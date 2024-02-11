using UnityEngine;
using UnityEngine.Events;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggered;

    [SerializeField] private Animator[] animators;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Animator animator in animators)
            {
                animator.enabled = true;
            }

            OnTriggered?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
