using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputManager))]
public class PlayerController : MonoBehaviour
{
    public InputManager im;
    public List<WheelCollider> throttleWheels;
    public List<GameObject> steeringWheels;
    public List<GameObject> meshes;
    public float strenghtCoefficient = 20000f;
    public float brakeCoefficient = 2f;
    public float maxTurn = 20f;
    public Transform CM;
    public Rigidbody rb;

    void Start()
    {
        im = GetComponent<InputManager>();
        //rb.centerOfMass = CM.position;
    }

    void FixedUpdate()
    {

        foreach (WheelCollider wheel in throttleWheels)
        {
            
            if (im.brake)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakeCoefficient * Time.deltaTime;
            } else
            {
                wheel.motorTorque = strenghtCoefficient * Time.deltaTime * im.throttle;
            }
        }

        foreach (GameObject wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            wheel.transform.localEulerAngles = new Vector3(0f, im.steer *maxTurn, 0f);
        }

        foreach (GameObject mesh in meshes)
        {
            mesh.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.33f), 0f, 0f);
        }
    }



}

