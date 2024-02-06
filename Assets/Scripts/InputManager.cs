using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private Controls controls;

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

        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public bool IntroAnyKeyPressed()
    {
        return controls.Intro.AnyKey.triggered;
    }

    public Vector2 GetIntroWASDPress()
    {
        return controls.Intro.WASD.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerMovement()
    {
        return controls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerLooking()
    {
        return controls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerShot()
    {
        return controls.Player.Shoot.triggered;
    }
}
