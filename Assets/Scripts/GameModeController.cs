using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameModeController : StateMachine
{
    public GameObject player;
    public Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("first layer");
        SetGameState(new StartGame(this));
    }
}
