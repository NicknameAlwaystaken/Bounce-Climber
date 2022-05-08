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
        //player.BouncingAllowed = true;
        player.MovingAllowed = true;
        player.DoubleJumpingAllowed = true;
        player.enabled = true;
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
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                player.BouncingAllowed = !player.BouncingAllowed;
                if(player.BouncingAllowed)
                {
                    SetPlayerState(new Bouncing(this, player));
                    player.Landed = false;
                }
            }
            if (player.DoubleJumping)
            {
                SetPlayerState(new DoubleJumping(this, player));
            }
            if (player.Moving)
            {
                SetPlayerState(new Moving(this, player));
            }
            if (player.Landed)
            {
                player.Jumping = player.JumpingAllowed && player.MovingUp;
                player.Bouncing = player.BouncingAllowed;

                if (player.Jumping)
                {
                    SetPlayerState(new Jumping(this, player));
                    player.Landed = false;
                }
                else if (player.Bouncing)
                {
                    SetPlayerState(new Bouncing(this, player));
                    player.Landed = false;
                }
            }
        }
    }
}
