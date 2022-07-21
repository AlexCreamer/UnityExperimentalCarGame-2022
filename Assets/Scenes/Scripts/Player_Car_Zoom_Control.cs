using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Mirror;

public class Player_Car_Zoom_Control: NetworkBehaviour
{
    #region +++++++++++ VARAIBLE DELCARATIONS BEGIN +++++++++++
    private CinemachineFreeLook freeLookCam;

    private GameObject player;

    [SerializeField] private InputActionAsset inputProvider;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float zoomAcceleration = 2.5f;
    [SerializeField] private float zoomInnerRange = 3;
    [SerializeField] private float zoomOuterRange = 50;

    [SerializeField] private float zoomYAxis = 0f;

    public float ZoomYAxis
    {
        get { return zoomYAxis; }
        set
        {
            if (zoomYAxis == value) return ;
            zoomYAxis = value;
            AdjustCameraZoomIndex(zoomYAxis);
        }
    }


    [SerializeField] private CinemachineFreeLook freeLookCameraToZoom;
    private float currentMiddleRigRadius = 10f;
    private float newMiddleRigRadius = 10f;

    #endregion +++++++++++ VARIABLE DECLARATIONS END +++++++++++


    private void Awake()
    {
       inputProvider.FindActionMap("CM FreeLook1 Camera Controls").FindAction("Mouse Zoom").performed += cntxt => ZoomYAxis = cntxt.ReadValue<float>();
       inputProvider.FindActionMap("CM FreeLook1 Camera Controls").FindAction("Mouse Zoom").canceled += cntxt => ZoomYAxis = 0f;
    }

    private void Start()
    {
        var freeLookCam = GetComponent<CinemachineFreeLook>(); 

        if(isLocalPlayer)
        {
            freeLookCameraToZoom = CinemachineFreeLook.FindObjectOfType<CinemachineFreeLook>();
        }
    }

    private void OnEnable() 
    {
        inputProvider.FindAction("Mouse Zoom").Enable();
    }

    private void OnDisable()
    {
        inputProvider.FindAction("Mouse Zoom").Disable();
    }

    private void UpdateZoomLevel()
    {
        if (currentMiddleRigRadius == newMiddleRigRadius) { return; }

        currentMiddleRigRadius = Mathf.Lerp(currentMiddleRigRadius, newMiddleRigRadius, zoomAcceleration * Time.deltaTime);
        currentMiddleRigRadius = Mathf.Clamp(currentMiddleRigRadius, zoomInnerRange, zoomOuterRange);

        freeLookCameraToZoom.m_Orbits[1].m_Radius = currentMiddleRigRadius;
        freeLookCameraToZoom.m_Orbits[0].m_Height = freeLookCameraToZoom.m_Orbits[1].m_Radius;
        freeLookCameraToZoom.m_Orbits[2].m_Height = -freeLookCameraToZoom.m_Orbits[1].m_Radius;
    }

    public void AdjustCameraZoomIndex(float zoomYAxis)
    {
        if (zoomYAxis == 0) { return; }

        if (zoomYAxis < 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius + zoomSpeed;
        }

        if (zoomYAxis > 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius - zoomSpeed;
        }
    }

    private void LateUpdate()
    {
        UpdateZoomLevel();

        freeLookCam.Follow = player.transform;
        freeLookCam.LookAt = player.transform;
    }
      
}