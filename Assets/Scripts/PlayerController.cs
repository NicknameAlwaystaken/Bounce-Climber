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
        SetPlayerActions();
        SetPlayerSettings();
    }

    private void SetPlayerActions()
    {
        SetPlayerState(new Bouncing(this, player));
        SetPlayerState(new Moving(this, player));
        SetPlayerState(new Jumping(this, player));
        SetPlayerState(new DoubleJumping(this, player));
        SetPlayerState(new Enable(this, player));
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
                SetPlayerState(new Jumping(this, player));
                StartCoroutine(PlayerState.Jump());
            }
            if (player.DoubleJumping)
            {
                SetPlayerState(new DoubleJumping(this, player));
                StartCoroutine(PlayerState.DoubleJump());
            }
            if (player.Bouncing)
            {
                SetPlayerState(new Bouncing(this, player));
                StartCoroutine(PlayerState.Bounce());
            }
            if (player.Moving)
            {
                SetPlayerState(new Moving(this, player));
                StartCoroutine(PlayerState.Move());
            }
        }
    }
}
