using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutroText : Menu
{
    protected override void GetAnyKeyPress()
    {
        if (inputManager.MenuAnyKeyPressed())
        {
            nextButton = NextExpectedButtonPress.e;
            audioManagerSource.PlayOneShot(audioClips[0]);
            letters[0].enabled = true;
        }
    }

    protected override void GetLettersPress()
    {
        Vector2 endPress = inputManager.GetMenuENDPress();

        switch (nextButton)
        {
            case NextExpectedButtonPress.e:
                if (endPress.x < 0f)
                {
                    nextButton = NextExpectedButtonPress.n;
                    audioManagerSource.PlayOneShot(audioClips[1]);
                    letters[1].enabled = true;
                }
                break;
            case NextExpectedButtonPress.n:
                if (endPress.y < 0f)
                {
                    nextButton = NextExpectedButtonPress.d;
                    audioManagerSource.PlayOneShot(audioClips[2]);
                    letters[2].enabled = true;
                }
                break;
            case NextExpectedButtonPress.d:
                if (endPress.x > 0f)
                {
                    GameManager.Instance.LoadNextLevel();
                }
                break;
        }
    }
}
