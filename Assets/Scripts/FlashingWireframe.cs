using UnityEngine;

public class FlashingWireframe : MonoBehaviour
{
    [SerializeField] private GameObject levelWireframe;

    private Renderer[] levelInteriorRenderers;

    private void Start()
    {
        levelInteriorRenderers = GetComponentsInChildren<Renderer>();
        AudioManager.Instance.OnKick += ToggleWireframe;
    }

    private void ToggleWireframe()
    {
        levelWireframe.SetActive(!levelWireframe.activeSelf);

        foreach (Renderer renderer in levelInteriorRenderers)
        {
            renderer.enabled = !renderer.enabled;
        }
    }
}
