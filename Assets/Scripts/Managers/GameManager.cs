using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform playerBody;
    public Transform playerHead;

    [SerializeField] private Noise noise;
    [SerializeField] private Gun playerPistol;
    [SerializeField] private Animator crosshairAnimator;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Application.Quit();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FadeInExitNoise()
    {
        noise.FadeInAudio();
    }

    public void PutPlayerGunAway()
    {
        playerPistol.PutGunAway();
        crosshairAnimator.SetTrigger("Fade Out");
    }
}
