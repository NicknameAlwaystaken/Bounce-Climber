using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateMachine
{
    private Player player;
    public PlayerSpawner playerSpawner;
    public Vector3 playerSpawnLocation;

    public void SpawnPlayer()
    {
        player = playerSpawner.SpawnPlayer(playerSpawnLocation).GetComponent<Player>();
        SetPlayerState(new Spawning(this, player));
        SetPlayerSettings();
    }

    private void SetPlayerSettings()
    {
        player.MaxMovementSpeed = 30f;
        player.BounceVelocity = 30f;
        player.SuperJumpIncrement = 1.5f;
        player.LowJumpIncrement = 0.8f;
    }

    public void Update()
    {
        if (PlayerState != null)
        {
            StartCoroutine(PlayerState.Update());
            if (player.Jumping)
            {
                StartCoroutine(PlayerState.Jump());
            }
            if (player.Moving)
            {
                StartCoroutine(PlayerState.Move());
            }
        }
    }
}
