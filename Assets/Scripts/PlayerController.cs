using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateMachine
{
    private Player Player;
    [SerializeField] private PlayerSpawner playerSpawner;

    public Vector3 playerSpawnLocation;
    private int currentScore;



    public Player SpawnPlayer()
    {
        Player = playerSpawner.SpawnPlayer(playerSpawnLocation).GetComponent<Player>();
        SetPlayerActions();
        SetPlayerSettings();
        return Player;
    }

    private void SetPlayerActions()
    {
        Player.JumpingAllowed = true;
        Player.DashingAllowed = true;
        Player.MovingAllowed = true;
        Player.DoubleJumpingAllowed = true;
        Player.enabled = true;
    }

    private void SetPlayerSettings()
    {
        Player.MaxMovementSpeed = 30f;
        Player.BounceVelocity = 30f;
        Player.FirstJumpIncrement = 1.2f;
        Player.DoubleJumpIncrement = 0.8f;
        Player.AutoJumpBounceVelocity = 0.5f;
        Player.DashingDistance = 20f;
        Player.DashingHorizontalTimer = 5.0f;
        Player.DashingVerticalTimer = 10.0f;
        Player.DashingLift = 15f;
        Player.DashFreeingDistance = 0.5f;
        Player.CurrentScore = 0f;
    }

    public Player GetPlayer()
    {
        return Player;
    }

    public void Update()
    {
        if (Player != null)
        {
            if(Player.HasContact && Player.LastContact != null)
            {
                Player.HasContact = false;
                GameController.instance.DestroyBreakable(Player.LastContact);
            }
            if(PlayerState != null) StartCoroutine(PlayerState.Update());
            if(Player.Dashing)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    StartCoroutine(PlayerState.StopDash());
                    return;
                }
                else return;
            }
            if (Player.ToggleBounce)
            {
                Player.BouncingAllowed = !Player.BouncingAllowed;
                if(Player.BouncingAllowed && Player.Landed)
                {
                    SetPlayerState(new Bouncing(this, Player));
                    Player.Landed = false;
                }
                Player.ToggleBounce = false;
            }
            if (Player.DoubleJumping)
            {
                SetPlayerState(new DoubleJumping(this, Player));
            }
            if (Player.Moving)
            {
                SetPlayerState(new Moving(this, Player));
            }
            if (Player.DashingConditions)
            {
                SetPlayerState(new Dashing(this, Player));
                PlayerState.Start();
                return;
            }
            if (Player.Landed)
            {
                Player.Jumping = Player.JumpingAllowed && Player.MovingUp;
                Player.Bouncing = Player.BouncingAllowed;

                if (Player.Jumping)
                {
                    SetPlayerState(new Jumping(this, Player));
                    Player.Landed = false;
                }
                else if (Player.Bouncing && !Player.BouncingDone)
                {
                    SetPlayerState(new Bouncing(this, Player));
                    Player.BouncingDone = true;
                }
            }
        }
    }
    public void FixedUpdate()
    {
        if (PlayerState != null)
        {
            StartCoroutine(PlayerState.FixedUpdate());
            if (Player.Dashing) StartCoroutine(PlayerState.Dash());
        }
    }
}
