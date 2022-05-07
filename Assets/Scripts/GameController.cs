using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : StateMachine
{
    public PlayerController playerController;

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("first layer");
        SetGameState(new StartGame(this));
    }

    public void SpawnPlayer()
    {
        playerController.SpawnPlayer();
    }
    public virtual void Update()
    {
        if (GameState != null) StartCoroutine(GameState.Update());
    }
}
