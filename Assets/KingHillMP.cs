using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHillMP : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject carObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int numberOfPlayers = PhotonNetwork.CountOfPlayers % 10; //FIX THIS
        GameObject eu = PhotonNetwork.Instantiate(carObj.name, new Vector3(0, numberOfPlayers* 50), Quaternion.identity);

        Camera.main.GetComponent<CameraManager>().focus = eu;
    }
}
