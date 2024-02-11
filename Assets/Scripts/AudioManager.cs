using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public event Action OnKick;

    [SerializeField] private AudioClip kick;
    [SerializeField, Range(1, 1000)] private int BPM = 120;

    private AudioSource audioSource;
    private Coroutine kickCoroutine;
    private bool kicksOnTwo = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Kick(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            audioSource.PlayOneShot(kick);
            kicksOnTwo = !kicksOnTwo;
            OnKick?.Invoke();

            yield return new WaitForSeconds(60f / BPM);
        }
    }

    public void StartKicks(float delay)
    {
        kickCoroutine = StartCoroutine(Kick(delay));
    }

    public void StopKicks()
    {
        if (kicksOnTwo)
        {
            StopCoroutine(kickCoroutine);
        }
        else
        {
            OnKick += StopKicksOnTwo;
        }
    }

    private void StopKicksOnTwo()
    {
        StopKicks();
        OnKick -= StopKicksOnTwo;
    }
}
