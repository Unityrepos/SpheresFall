using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1;
    [SerializeField]
    private float minYRotation  = -30;
    [SerializeField]
    private float maxYRotation  = 60;
    
    private float yRotation = 0;

    void Update()
    {
        yRotation = Mathf.Clamp (yRotation - (Input.GetAxis ("Mouse Y") * Time.deltaTime * rotationSpeed), minYRotation, maxYRotation);
        transform.localEulerAngles = new Vector3 (yRotation, 0, 0);
    }
}
