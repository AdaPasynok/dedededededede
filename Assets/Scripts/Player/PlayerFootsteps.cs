using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float timeBetweenSteps = 1f;
    [SerializeField, Range(-1f, 1f)] private float pitchRange = 0.2f;

    private InputManager inputManager;
    private AudioSource audioSource;
    private float initialPitch;
    private float timeElapsed;

    private void Start()
    {
        inputManager = InputManager.Instance;
        audioSource = GetComponent<AudioSource>();
        initialPitch = audioSource.pitch;
        timeElapsed = timeBetweenSteps;
    }

    private void Update()
    {
        if (inputManager.GetPlayerMovement() != Vector2.zero)
        {
            if (timeElapsed >= timeBetweenSteps)
            {
                timeElapsed = 0f;
                audioSource.pitch = Random.Range(initialPitch - pitchRange, initialPitch + pitchRange);
                audioSource.PlayOneShot(audioClip);
            }
            else
            {
                timeElapsed += Time.deltaTime;
            }
        }
    }
}
