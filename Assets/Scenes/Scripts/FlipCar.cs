using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCar : MonoBehaviour
{
    [SerializeField] private GameObject car;

    [SerializeField] private float maxAngle = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((car.transform.rotation.eulerAngles.z > maxAngle) || (car.transform.rotation.eulerAngles.z < -maxAngle))
        {
            car.transform.Rotate(0, 0, -car.transform.rotation.eulerAngles.z);
        }
    }
}
