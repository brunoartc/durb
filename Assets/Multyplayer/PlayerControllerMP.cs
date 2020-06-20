using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputManagerMP))]
public class PlayerControllerMP : MonoBehaviour, IPunObservable

{


    public createGrid terrainScript;
    public InputManagerMP im;
    public List<WheelCollider> throttleWheels;
    public List<GameObject> steeringWheels;
    public List<GameObject> meshes;
    public float strenghtCoefficient = 20000f;
    public float brakeCoefficient = 2f;
    public float maxTurn = 20f;
    public Transform CM;
    public Rigidbody rb;


    public Tuple<float, float> localPlayerPosition;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo msg)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(localPlayerPosition.Item1);
            stream.SendNext(localPlayerPosition.Item2);
            Tuple<float, float> lloPlayerPosition = new Tuple<float, float>(localPlayerPosition.Item1, localPlayerPosition.Item2);
            //Debug.Log($"sending x={localPlayerPosition.Item1} y={localPlayerPosition.Item2}");
            terrainScript.updateDictionary(GetComponent<PhotonView>().ViewID.ToString(), lloPlayerPosition);

        }
        else
        {
            float x = (float)stream.ReceiveNext();
            float y = (float)stream.ReceiveNext();
            //Debug.Log($"{GetComponent<PhotonView>().ViewID} sent x={x} y={y}");
            Tuple<float, float> remotePlayerPosition  = new Tuple<float, float>(x, y);
            terrainScript.updateDictionary(GetComponent<PhotonView>().ViewID.ToString(), remotePlayerPosition);
        }
    }

    void Start()
    {
        im = GetComponent<InputManagerMP>();
        terrainScript = GameObject.FindGameObjectWithTag("Terrain").GetComponent<createGrid>();
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
            }
            else
            {
                wheel.motorTorque = strenghtCoefficient * Time.deltaTime * im.throttle;
            }
        }


        foreach (GameObject wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            wheel.transform.localEulerAngles = new Vector3(0f, im.steer * maxTurn, 0f);
        }

        foreach (GameObject mesh in meshes)
        {
            mesh.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.33f), 0f, 0f);
        }


        localPlayerPosition =  new Tuple<float, float>(transform.position.x, transform.position.y);

    }



}

