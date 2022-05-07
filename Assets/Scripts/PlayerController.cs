using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateMachine
{
    private Player player;
    public PlayerSpawner playerSpawner;
    public Vector3 playerSpawnLocation;

    public Player SpawnPlayer()
    {
        player = playerSpawner.SpawnPlayer(playerSpawnLocation).GetComponent<Player>();
        SetPlayerState(new Spawning(this, player));
        player.MaxMovementSpeed = 30f;
        return player;
    }
    public void Update()
    {
        if (PlayerState != null) StartCoroutine(PlayerState.Update());
        if (player.Jumping) StartCoroutine(PlayerState.Jump());
        if (player.JumpingDone) StopCoroutine(PlayerState.Jump());
    }
}
