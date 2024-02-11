using TMPro;
using UnityEngine;

public class IntroText : Menu
{
    [SerializeField] private TextMeshProUGUI de;
    [SerializeField] private AudioClip deAudio;

    private int deCount = 0;

    protected override void GetAnyKeyPress()
    {
        if (inputManager.MenuAnyKeyPressed())
        {
            if (deCount == 46)
            {
                nextButton = NextExpectedButtonPress.w;
                audioManagerSource.PlayOneShot(audioClips[0]);
                letters[0].enabled = true;
            }
            else
            {
                audioManagerSource.PlayOneShot(deAudio);
                de.text += "DE ";
                deCount++;
            }
        }
    }

    protected override void GetLettersPress()
    {
        Vector2 wasdPress = inputManager.GetMenuWASDPress();

        switch (nextButton)
        {
            case NextExpectedButtonPress.w:
                if (wasdPress.y > 0f)
                {
                    nextButton = NextExpectedButtonPress.a;
                    audioManagerSource.PlayOneShot(audioClips[1]);
                    letters[1].enabled = true;
                }
                break;
            case NextExpectedButtonPress.a:
                if (wasdPress.x < 0f)
                {
                    nextButton = NextExpectedButtonPress.s;
                    audioManagerSource.PlayOneShot(audioClips[2]);
                    letters[2].enabled = true;
                }
                break;
            case NextExpectedButtonPress.s:
                if (wasdPress.y < 0f)
                {
                    nextButton = NextExpectedButtonPress.d;
                    audioManagerSource.PlayOneShot(deAudio);
                    letters[3].enabled = true;
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
