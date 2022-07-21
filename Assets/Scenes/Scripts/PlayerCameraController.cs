//https://www.youtube.com/watch?v=B4vNWUTQues&t=625s
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;
    [SerializeField] private InputActionAsset inputProvider;
    private CinemachineTransposer transposer;

    private InputActionAsset Controls
    {
        get
        {
            if(inputProvider != null) { return inputProvider;}
            return inputProvider = new InputActionAsset();
        }
    }


    public override void OnStartAuthority()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualCamera.gameObject.SetActive(true);

        enabled = true;
        inputProvider.FindActionMap("CM FreeLook1 Camera Controls").FindAction("MouseLook").performed += ctx => Look(ctx.ReadValue<Vector2>());

    }

    [ClientCallback]
    private void OnEnable() => inputProvider.Enable();
    [ClientCallback]
    private void OnDisable() => inputProvider.Disable();

    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;

        float followOffset = Mathf.Clamp(
            transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * deltaTime),
            maxFollowOffset.x,
            maxFollowOffset.y);

        transposer.m_FollowOffset.y = followOffset;

        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
    }

}


