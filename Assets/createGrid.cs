﻿using System.Collections;
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

    public List<GameObject> players;
    public Dictionary<string, int> playersPoints;
    string[,] owners;


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
        foreach (GameObject player in players)
        {

            int x = (int) ((-minXGrid + player.transform.position.x) / chunkXArea);
            int z = (int) ((-minZGrid + player.transform.position.z) / chunkZArea);
            Debug.Log("player <" + player.tag + "> captured [" + x + "," + z + "];");
            if (player.tag != owners[x, z])
            {
                string oldplayer = owners[x, z];
                owners[x, z] = player.tag;

                playersPoints[player.tag] += 1;
                playersPoints[oldplayer] -= 1;


                points.GetComponent<Text>().text = string.Format("Points: {0}", playersPoints[player.tag]);
                Debug.Log("player <" + player.tag + "> captured [" + x + "," + z + "];");
            }



            

            //player.GetComponent<>






        }
        
    }
}