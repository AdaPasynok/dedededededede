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

    private void CompleteLevel()
    {
        GameManager.Instance.FadeInExitNoise();
        audioManager.StopKicks();
        enemiesFollowing.SetActive(false);
        enemiesAtExit.SetActive(true);
    }

    public void CompleteLevelAfterNextLightOn()
    {
        audioManager.OnKick += CompleteLevel;
    }
}
