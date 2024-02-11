using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1f;

    private enum FadeType
    {
        In,
        Out
    }

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FadeInAudio()
    {
        StartCoroutine(FadeAudio(FadeType.In));
    }

    public void FadeOutAudio()
    {
        StartCoroutine(FadeAudio(FadeType.Out));
    }

    private IEnumerator FadeAudio(FadeType fadeType)
    {
        switch (fadeType)
        {
            case FadeType.In:
                while (audioSource.volume < 1f)
                {
                    audioSource.volume += 1f / fadeTime * Time.deltaTime;

                    yield return null;
                }
                break;
            case FadeType.Out:
                while (audioSource.volume > 0f)
                {
                    audioSource.volume -= 1f / fadeTime * Time.deltaTime;

                    yield return null;
                }
                break;
        }
    }
}
