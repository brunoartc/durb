using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class createGrid : MonoBehaviour
{
    public GameObject points;
    
    

    float minXGrid = -250.0f;
    float minZGrid = -250.0f;
    float gridXSize = 500.0f;
    float gridZSize = 500.0f;


    float chunkXArea = 50.0f;
    float chunkZArea = 50.0f;

    [SerializeField] public List<GameObject> players;
    [SerializeField] public Dictionary<string, int> playersPoints;
    [SerializeField] string[,] owners;
    [SerializeField] public Dictionary<string, Tuple<float, float>> playersPositions;


    public void updateDictionary(string id, Tuple<float, float> position)
    {
        playersPositions.Add(id, position);
    }

    void Start()
    {
        owners = new string[(int) (gridXSize/ chunkXArea), (int) (gridZSize/ chunkZArea)];
        int totalPoints = 0;
        for (int i = 0; i < (int)(gridXSize / chunkXArea); i++)
        {
            for (int j = i; j < (int)(gridZSize / chunkZArea); j++)
            {
                totalPoints++;
                owners[i, j] = "Game";
            }
        }
        playersPoints = new Dictionary<string, int>();
        playersPoints.Add("Game", totalPoints);
        
        foreach (GameObject player in players)
        {
            playersPoints.Add(player.tag, 0);
        }
    }

    void Update()
    {
        foreach (KeyValuePair<string, Tuple<float, float>> player in playersPositions)
        {

            int x = (int) ((-minXGrid + player.Value.Item1) / chunkXArea);
            int z = (int) ((-minZGrid + player.Value.Item2) / chunkZArea);
            Debug.Log("player <" + player.Key + "> captured [" + x + "," + z + "];");
            if (!playersPoints.ContainsKey(player.Key))
            {
                playersPoints.Add(player.Key, 0);
            }
            if (player.Key != owners[x, z])
            {
                string oldplayer = owners[x, z];
                owners[x, z] = player.Key;

                playersPoints[player.Key] += 1;
                playersPoints[oldplayer] -= 1;


                points.GetComponent<Text>().text = string.Format("Points: {0}", playersPoints[player.Key]);
                Debug.Log("player <" + player.Key + "> captured [" + x + "," + z + "];");
            }



            

            //player.GetComponent<>






        }
        
    }
}
