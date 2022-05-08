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
        player.FirstJumpIncrement = 1.2f;
        player.DoubleJumpIncrement = 0.8f;
        player.AutoJumpBounceVelocity = 0.5f;
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
            if (player.DoubleJumping)
            {
                StartCoroutine(PlayerState.DoubleJump());
            }
            if (player.Bouncing)
            {
                StartCoroutine(PlayerState.Bounce());
            }
            if (player.Moving)
            {
                StartCoroutine(PlayerState.Move());
            }
        }
    }
}
