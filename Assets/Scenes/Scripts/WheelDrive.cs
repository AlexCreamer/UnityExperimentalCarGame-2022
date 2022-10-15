using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum DriveType { RearWheelDrive, FrontWheelDrive, AllWheelDrive }
public class WheelDrive : NetworkBehaviour
{
    [SerializeField] float maxAngle = 30f;
    [SerializeField] float maxTorque = 30f;
    [SerializeField] float brakeTorque = 30000f;
    [SerializeField] GameObject wheelShape;
    [SerializeField] GameObject player;
    
    [SerializeField] float criticalSpeed = 5f;
    [SerializeField] int stepBelow = 5;
    [SerializeField] int stepAbove = 1;
    
    [SerializeField] DriveType driveType;
    WheelCollider[] m_Wheels;
    float handBrake, torque;
    public float angle;

    // Input Actions
    public InputActionAsset inputActions;
    InputActionMap gameplayActionMap;
    InputAction handBrakeInputAction;
    InputAction steeringInputAction;
    InputAction accelerationInputAction;

    [SerializeField] Vector3 allRotationOffsets;

    private Vector3 smoothInputVelocity;

    [SerializeField] private float smoothInputSpeed = .2f;

    private void Awake() 
    {
        gameplayActionMap = inputActions.FindActionMap("Gameplay Movement");

        handBrakeInputAction = gameplayActionMap.FindAction("HandBrake");
        steeringInputAction = gameplayActionMap.FindAction("SteeringAngle");
        accelerationInputAction = gameplayActionMap.FindAction("Acceleration");

        handBrakeInputAction.performed += GetHandBrakeInput;
        handBrakeInputAction.canceled += GetHandBrakeInput;

        steeringInputAction.performed += GetAngleInput;
        steeringInputAction.canceled += GetAngleInput;

        accelerationInputAction.performed += GetTorqueInput;
        accelerationInputAction.canceled += GetTorqueInput;

    }

    void GetHandBrakeInput(InputAction.CallbackContext context)
    {
        handBrake = context.ReadValue<float>() * brakeTorque;
    }

    void GetAngleInput(InputAction.CallbackContext context)
    {
        angle = context.ReadValue<float>() * maxAngle;
    }

    void GetTorqueInput(InputAction.CallbackContext context)
    {
        torque = context.ReadValue<float>() * maxTorque;
    } 

    private void OnEnable() 
    {
        handBrakeInputAction.Enable();
        steeringInputAction.Enable();
        accelerationInputAction.Enable();
    }

    private void onDisable()
    {    
        handBrakeInputAction.Disable();
        steeringInputAction.Disable();
        accelerationInputAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
         player.SetActive(false);
    }

    public void Movement()
    {
        m_Wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepBelow, stepAbove);

        foreach (WheelCollider wheel in m_Wheels)
        {
            if (wheel.transform.localPosition.z > 0)
            {
                wheel.steerAngle = angle;
            }
            if (wheel.transform.localPosition.z < 0)
            {
                wheel.brakeTorque = handBrake;
            }
            if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
            {
                wheel.motorTorque = torque;
            }
            if (wheel.transform.localPosition.z > 0 && driveType != DriveType.FrontWheelDrive)
            {
                wheel.motorTorque = torque;
            }
            if (wheelShape)
            {
                Quaternion q;
                Vector3 p;
                wheel.GetWorldPose(out p, out q);

                Transform shapeTransform = wheel.transform.GetChild(0);

                if (wheel.name == "a0l" || wheel.name == "a1l" || wheel.name == "a0r" || wheel.name == "a1r")
                {
                    shapeTransform.rotation = q;
                    shapeTransform.position = Vector3.SmoothDamp(shapeTransform.position, p, ref smoothInputVelocity, smoothInputSpeed, criticalSpeed);
                    shapeTransform.localRotation *= Quaternion.Euler(allRotationOffsets);
                }
                else
                {
                    shapeTransform.position = p;
                    shapeTransform.rotation = q;
                }

            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Car Game")
        {
            if (player.activeSelf == false)
            {
                player.SetActive(true);
                m_Wheels = GetComponentsInChildren<WheelCollider>();
                for (int i = 0; i < m_Wheels.Length; i++)
                {
                    var wheel = m_Wheels[i];
                    if (wheelShape != null)
                    {
                        var ws = Instantiate(wheelShape, wheel.transform.position, Quaternion.Euler(new Vector3(90, 90, 90)));
                        ws.transform.parent = wheel.transform;
                    }
                }
            }
            if (hasAuthority)
            {
                Movement();
            }
        }
        else
        {
            return;
        }
    }
}
