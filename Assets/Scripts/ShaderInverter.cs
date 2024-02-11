using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderInverter : MonoBehaviour
{
    [SerializeField] private bool invertOnStart = true;

    public bool isInverting { get; private set; } = false;

    private AudioManager audioManager;
    private Renderer[] renderers;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        renderers = GetComponentsInChildren<Renderer>();

        if (invertOnStart)
        {
            StartInverting();
        }
    }

    private void InvertShader()
    {
        foreach (Renderer renderer in renderers)
        {
            int isInverted = renderer.material.GetInt("_IsInverted") == 1 ? 0 : 1;
            renderer.material.SetInt("_IsInverted", isInverted);
        }
    }

    public void StartInverting()
    {
        isInverting = true;
        audioManager.OnKick += InvertShader;
    }

    public void StopInverting()
    {
        if (renderers[0].material.GetInt("_IsInverted") == 0)
        {
            InvertShader();
        }

        isInverting = false;
        audioManager.OnKick -= InvertShader;
    }
}
