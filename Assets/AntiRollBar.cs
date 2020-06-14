using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;

public class AntiRollBar : MonoBehaviour //FROM https://gamedev.stackexchange.com/questions/118388/how-to-do-an-anti-sway-bar-for-a-car-in-unity-5
{

    public WheelCollider WheelL;
    public WheelCollider WheelR;
    private Rigidbody carRigidBody;
    public float AntiRoll = 5000.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void fixedUpdate()
    {

        WheelHit hit = new WheelHit();
        float travelL = 1.0f;
        float travelR = 1.0f;

        var groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL)
        {
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;
        }
            

        var groundedR = WheelR.GetGroundHit(out hit);
        if (groundedR)
        {
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;
        }
            

        var antiRollForce = (travelL - travelR) * AntiRoll;

        if (groundedL)
        {
            carRigidBody.AddForceAtPosition(WheelL.transform.up * -antiRollForce,
                                         WheelL.transform.position);
        }
            
        if (groundedR)
        {
            carRigidBody.AddForceAtPosition(WheelR.transform.up * antiRollForce,
                                         WheelR.transform.position);
        }
            
    }
}
