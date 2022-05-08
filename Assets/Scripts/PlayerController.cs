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
        player.JumpingAllowed = true;
        player.BouncingAllowed = true;
        player.MovingAllowed = true;
        player.DoubleJumpingAllowed = true;
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
            if (player.Jumping)
            {
                SetPlayerState(new Jumping(this, player));
            }
            if (player.DoubleJumping)
            {
                SetPlayerState(new DoubleJumping(this, player));
            }
            if (player.Bouncing)
            {
                SetPlayerState(new Bouncing(this, player));
            }
            if (player.Moving)
            {
                SetPlayerState(new Moving(this, player));
            }
        }
    }
}
