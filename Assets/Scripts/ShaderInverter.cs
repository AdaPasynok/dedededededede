using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderInverter : MonoBehaviour
{
    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        AudioManager.Instance.OnKick += InvertShader;
    }

    private void InvertShader()
    {
        int isInverted = material.GetInt("_IsInverted") == 1 ? 0 : 1;
        material.SetInt("_IsInverted", isInverted);
    }
}
