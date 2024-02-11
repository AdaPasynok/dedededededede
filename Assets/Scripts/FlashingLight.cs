using UnityEngine;
using UnityEngine.UI;

public class FlashingLight : MonoBehaviour
{
    private Image blackImage;

    private void Start()
    {
        blackImage = GetComponent<Image>();
        AudioManager.Instance.OnKick += () => blackImage.enabled = !blackImage.enabled;
    }
}
