using System.Collections;
using UnityEngine;

public class NoRotator : MonoBehaviour
{
    [SerializeField] private int speedModifier = 30;
    [SerializeField, Range(1, 10)] private int shotsRequired = 5;
    [SerializeField] private float fallThroughFloorDelay = 1f;
    [SerializeField] private GameObject stairsCeiling;
    [SerializeField] private MeshRenderer fadingWallRenderer;
    [SerializeField] private float fadeTime = 1f;

    private GameManager gameManager;
    private AudioManager audioManager;
    private ShaderInverter[] childEnemiesShaderInverters;
    private bool isRotating = false;
    private int initialBPM;
    private int shotsMade = 0;
    private Color oldWallColor, newWallColor;
    private float lerp = 0f;

    private void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        childEnemiesShaderInverters = GetComponentsInChildren<ShaderInverter>();
        initialBPM = audioManager.BPM;
        oldWallColor = newWallColor = fadingWallRenderer.material.color;
        newWallColor.a = 0f;
    }

    private void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Time.deltaTime * audioManager.BPM * Vector3.up);
        }
    }

    private void StartShaderInvertingOnRandomChild(ShaderInverter previoiusInverter)
    {
        ShaderInverter randomInverter;

        do
        {
            int randomIndex = Random.Range(0, childEnemiesShaderInverters.Length);
            randomInverter = childEnemiesShaderInverters[randomIndex];
        } while (randomInverter == previoiusInverter);

        randomInverter.StartInverting();
    }

    public void StartRotating()
    {
        isRotating = true;
        StartShaderInvertingOnRandomChild(null);
    }

    public void OnChildEnemyGotShot(ShaderInverter shaderInverter)
    {
        if (shaderInverter.isInverting)
        {
            shaderInverter.StopInverting();
            shotsMade++;

            if (shotsMade == shotsRequired)
            {
                gameManager.FadeInExitNoise();
                gameManager.PutPlayerGunAway();
                audioManager.StopKicks();
                isRotating = false;

                foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
                {
                    rigidbody.isKinematic = false;
                }

                stairsCeiling.SetActive(false);
                fadingWallRenderer.GetComponent<Collider>().enabled = false;
                StartCoroutine(ChildFallThroughFloor());
                StartCoroutine(FadeWall());
            }
            else
            {
                StartShaderInvertingOnRandomChild(shaderInverter);
                audioManager.BPM += speedModifier;
            }
        }
        else
        {
            audioManager.BPM = initialBPM;
            shotsMade = 0;
        }
    }

    private IEnumerator ChildFallThroughFloor()
    {
        yield return new WaitForSeconds(fallThroughFloorDelay);

        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.isTrigger = true;
        }

        StartCoroutine(DisableChild(10f));
    }

    private IEnumerator DisableChild(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (ShaderInverter childInverter in childEnemiesShaderInverters)
        {
            childInverter.gameObject.SetActive(false);
        }
    }

    private IEnumerator FadeWall()
    {
        while (lerp < 1f)
        {
            lerp += 1f / fadeTime * Time.deltaTime;
            fadingWallRenderer.material.color = Color.Lerp(oldWallColor, newWallColor, lerp);
            yield return null;
        }
    }
}
