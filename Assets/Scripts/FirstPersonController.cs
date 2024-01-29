using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform cameraMainTransform;
    [SerializeField] private CinemachineVirtualCamera cameraFPS;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float cameraSpeed = 0.3f;
    [SerializeField] private float gravityModifier = 1f;

    private CinemachinePOV cameraPOV;
    private InputManager inputManager;
    private CharacterController controller;
    private float gravity = -9.81f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cameraPOV = cameraFPS.GetCinemachineComponent<CinemachinePOV>();
        inputManager = InputManager.Instance;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        cameraPOV.m_VerticalAxis.m_MaxSpeed = cameraSpeed;
        cameraPOV.m_HorizontalAxis.m_MaxSpeed = cameraSpeed;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = cameraMainTransform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotation);

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 motion = transform.forward * movement.y + transform.right * movement.x;
        motion.y = gravity * gravityModifier;

        controller.Move(Time.deltaTime * movementSpeed * motion);
    }
}
