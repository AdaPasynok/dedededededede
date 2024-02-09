using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathLevelComplete : MonoBehaviour
{
    [SerializeField] private Image blackImage;
    [SerializeField] private GameObject enemiesFollowing;
    [SerializeField] private GameObject enemiesAtExit;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    private void LevelComplete()
    {
        audioManager.StopKicks();
        enemiesFollowing.SetActive(false);
        enemiesAtExit.SetActive(true);
    }

    public void LevelCompleteAfterNextLightOn()
    {
        if (blackImage.enabled)
        {
            audioManager.OnKick += LevelComplete;
        }
        else
        {
            audioManager.OnKick += LevelCompleteAfterNextLightOn;
        }
    }
}
