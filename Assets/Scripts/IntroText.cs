using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI de;
    [SerializeField] private AudioClip deAudio;
    [SerializeField] private TextMeshProUGUI[] wasd;
    [SerializeField] private AudioClip[] wasdAudio;
    [SerializeField] private float lightAngleSpeed = 2f;

    private enum NextExpectedButtonPress
    {
        any,
        w,
        a,
        s,
        d
    }

    private NextExpectedButtonPress nextButton = NextExpectedButtonPress.any;
    private AudioSource audioManagerSource;
    private InputManager inputManager;
    private Material wasdMaterial;
    private int deCount = 0;
    private float lightAngle = 3.1416f;

    private void Start()
    {
        audioManagerSource = AudioManager.Instance.GetComponent<AudioSource>();
        inputManager = InputManager.Instance;
        wasdMaterial = new Material(wasd[0].fontSharedMaterial);

        foreach (TextMeshProUGUI letter in wasd)
        {
            letter.fontMaterial = wasdMaterial;
        }
    }

    private void Update()
    {
        lightAngle -= Time.deltaTime * lightAngleSpeed;
        wasdMaterial.SetFloat("_LightAngle", lightAngle);

        if (nextButton == NextExpectedButtonPress.any)
        {
            GetAnyKeyPress();
        }
        else
        {
            GetWASDPress();
        }
    }

    private void GetAnyKeyPress()
    {
        if (inputManager.IntroAnyKeyPressed())
        {
            if (deCount == 46)
            {
                nextButton = NextExpectedButtonPress.w;
                audioManagerSource.PlayOneShot(wasdAudio[0]);
                wasd[0].enabled = true;
            }
            else
            {
                audioManagerSource.PlayOneShot(deAudio);
                de.text += "DE ";
                deCount++;
            }
        }
    }

    private void GetWASDPress()
    {
        Vector2 wasdPress = inputManager.GetIntroWASDPress();

        switch (nextButton)
        {
            case NextExpectedButtonPress.w:
                if (wasdPress.y > 0f)
                {
                    nextButton = NextExpectedButtonPress.a;
                    audioManagerSource.PlayOneShot(wasdAudio[1]);
                    wasd[1].enabled = true;
                }
                break;
            case NextExpectedButtonPress.a:
                if (wasdPress.x < 0f)
                {
                    nextButton = NextExpectedButtonPress.s;
                    audioManagerSource.PlayOneShot(wasdAudio[2]);
                    wasd[2].enabled = true;
                }
                break;
            case NextExpectedButtonPress.s:
                if (wasdPress.y < 0f)
                {
                    nextButton = NextExpectedButtonPress.d;
                    audioManagerSource.PlayOneShot(wasdAudio[3]);
                    wasd[3].enabled = true;
                }
                break;
            case NextExpectedButtonPress.d:
                if (wasdPress.x > 0f)
                {
                    GameManager.Instance.LoadNextLevel();
                }
                break;
        }
    }
}
