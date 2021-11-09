using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IRestartable
{
    [SerializeField]
    private float mainSpeed = 10;
    [SerializeField]
    private float maxSpeed  = 20;
    [SerializeField]
    private float minSpeed  = 7;

    private float       speed   = 10;
    private Rigidbody   rb      = null;

    void Start ()
    {
        Menu.RestartScripts.Add (this);
        
        rb = this.GetComponent<Rigidbody>();
    }
    void Update ()
    {
        if      (Input.GetKeyDown (KeyCode.LeftShift))
        {
            speed = maxSpeed;
        }
        else if (Input.GetKeyDown (KeyCode.LeftControl))
        {
            speed = minSpeed;
        }
        else if (Input.GetKeyUp (KeyCode.LeftControl) || Input.GetKeyUp (KeyCode.LeftShift))
        {
            speed = mainSpeed;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3 ( Input.GetAxis("Horizontal") * transform.right.x + Input.GetAxis("Vertical") * transform.forward.x, rb.velocity.y, 
                                    Input.GetAxis("Horizontal") * transform.right.z + Input.GetAxis("Vertical") * transform.forward.z) * speed * Time.fixedDeltaTime;
    }

    public void Restart ()
    {
        transform.position = new Vector3 (0, -15, 0);
    }
}
