using TMPro;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI[] letters;
    [SerializeField] protected AudioClip[] audioClips;

    [SerializeField] private float lightAngleSpeed = 5f;

    protected enum NextExpectedButtonPress
    {
        any,
        w,
        a,
        s,
        e,
        n,
        d
    }

    protected NextExpectedButtonPress nextButton = NextExpectedButtonPress.any;
    protected AudioSource audioManagerSource;
    protected InputManager inputManager;

    private Material lettersMaterial;
    private float lightAngle = 3.1416f;

    private void Start()
    {
        audioManagerSource = AudioManager.Instance.GetComponent<AudioSource>();
        inputManager = InputManager.Instance;
        lettersMaterial = new Material(letters[0].fontSharedMaterial);

        foreach (TextMeshProUGUI letter in letters)
        {
            letter.fontMaterial = lettersMaterial;
        }
    }

    private void Update()
    {
        lightAngle -= Time.deltaTime * lightAngleSpeed;
        lettersMaterial.SetFloat("_LightAngle", lightAngle);

        if (nextButton == NextExpectedButtonPress.any)
        {
            GetAnyKeyPress();
        }
        else
        {
            GetLettersPress();
        }
    }

    protected abstract void GetAnyKeyPress();

    protected abstract void GetLettersPress();
}
