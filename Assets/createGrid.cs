﻿using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class createGrid : MonoBehaviour
{
    public GameObject points;

    public TMPro.TextMeshProUGUI winingText;

    public List<GameObject> Cubos;

    private float startTime = 0.0f;

    float minXGrid = -250.0f;
    float minZGrid = -250.0f;
    float gridXSize = 500.0f;
    float gridZSize = 500.0f;

    bool flagMatch = true;

    float chunkXArea = 50.0f;
    float chunkZArea = 50.0f;

    public List<GameObject> players;
    public Dictionary<string, int> playersPoints;
    string[,] owners;
    public Dictionary<string, Tuple<float, float>> playersPositions;

    private int posToCube(int x, int z)
    {
        int cubo = z * 10 + x;
        return cubo;
    }

    private int idToColor(int id)
    {
        int color = (id % 1000) * (id % 1000) * (id % 1000);
        return color;
    }


    public void updateDictionary(string id, Tuple<float, float> position)
    {
        if (!playersPositions.ContainsKey(id))
        {
            playersPositions.Add(id, position);
        } else
        {
            playersPositions[id] = position;
        }
        
    }

    void Start()
    {
        playersPositions = new Dictionary<string, Tuple<float, float>>();
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
        //Debug.Log(playersPositions);

        if (Time.timeSinceLevelLoad > 180.0f)
        {
            if (flagMatch)
            {
                winingText.text = "Match End";
                foreach (KeyValuePair<string, int> pl in playersPoints)
                {
                    //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    winingText.text += string.Format("Player = {0}, Points = {1}", pl.Key, pl.Value);
                }
                flagMatch = !flagMatch;
            } else
            {
                if (Time.timeSinceLevelLoad > 200.0f)
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
            
        } else
        {
            foreach (KeyValuePair<string, Tuple<float, float>> player in playersPositions)
            {

                int x = (int)((-minXGrid + player.Value.Item1) / chunkXArea);
                int z = (int)((-minZGrid + player.Value.Item2) / chunkZArea);
                //Debug.Log("player <" + player.Key + "> captured [" + x + "," + z + "];");
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

                    //int color = idToColor(Int16.Parse(player.key));

                    int cube =  posToCube(x, z);

                    //Cubos[cube].renderer.material.color = new Color(color,1,1);
                    


                    points.GetComponent<Text>().text = string.Format("Points: {0}", playersPoints[player.Key]);
                    Debug.Log("player <" + player.Key + "> captured [" + x + "," + z + "];");
                }





                //player.GetComponent<>






            }
        }
        
        
    }
}
