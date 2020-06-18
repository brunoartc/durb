using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManagerMP : MonoBehaviourPun
{
    public float throttle;
    public float steer;

    public bool brake;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            throttle = Input.GetAxis("Vertical");

            steer = Input.GetAxis("Horizontal");

            brake = Input.GetKey(KeyCode.Space);
        }
    }
            
}
