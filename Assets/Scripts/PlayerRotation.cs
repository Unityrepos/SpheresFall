using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1;
    
    void Update()
    {
        transform.localEulerAngles += new Vector3 (0, Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * rotationSpeed;
    }
}
