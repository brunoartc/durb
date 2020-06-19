using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHillMP : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject carObj;
    void Start()
    {
        int numberOfPlayers = Int16.Parse(PhotonNetwork.NickName) % 10; //FIX THIS
        GameObject eu = PhotonNetwork.Instantiate(carObj.name, new Vector3(0, 0, numberOfPlayers * 50), Quaternion.identity);
        Camera.main.GetComponent<CameraManager>().focus = eu;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        

        
    }
}
