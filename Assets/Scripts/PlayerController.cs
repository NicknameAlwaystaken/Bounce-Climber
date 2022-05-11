using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateMachine
{
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private Player userInputSystem;

    public Vector3 playerSpawnLocation;
    private int currentScore;


    public Player SpawnPlayer()
    {
        //Player = playerSpawner.SpawnPlayer(playerSpawnLocation).GetComponent<Player>();
        userInputSystem = playerSpawner.SpawnPlayer(playerSpawnLocation).GetComponent<Player>();
        SetPlayerActions();
        SetPlayerSettings();
        return userInputSystem;
    }

    private void SetPlayerActions()
    {
        userInputSystem.JumpingAllowed = true;
        userInputSystem.DashingAllowed = true;
        userInputSystem.MovingAllowed = true;
        userInputSystem.DoubleJumpingAllowed = true;
        userInputSystem.enabled = true;
    }

    private void SetPlayerSettings()
    {
        userInputSystem.MaxMovementSpeed = 30f;
        userInputSystem.MovementSpeed = 200f;
        userInputSystem.SlowdownSpeed = 30f;
        userInputSystem.BounceVelocity = 30f;
        userInputSystem.FirstJumpIncrement = 1.2f;
        userInputSystem.DoubleJumpIncrement = 0.8f;
        userInputSystem.AutoJumpBounceVelocity = 0.5f;
        userInputSystem.DashingDistance = 20f;
        userInputSystem.DashingHorizontalTimer = 5.0f;
        userInputSystem.DashingVerticalTimer = 10.0f;
        userInputSystem.DashingLift = 15f;
        userInputSystem.DashFreeingDistance = 0.5f;
        userInputSystem.CurrentScore = 0f;
    }

    public Player GetPlayer()
    {
        return userInputSystem;
    }

    public void Update()
    {
        if (!IsGamePlaying())
        {
            if(userInputSystem != null)
            {
                userInputSystem.Paused = true;
            }
            return;
        }
        else
        {
            if (userInputSystem != null) userInputSystem.Paused = false;
        }

        if (userInputSystem != null && IsGamePlaying())
        {
            if(userInputSystem.HasContact && userInputSystem.LastContact != null)
            {
                userInputSystem.HasContact = false;
                GameController.instance.DestroyBreakable(userInputSystem.LastContact);
            }
            /*
            if(PlayerState != null) StartCoroutine(PlayerState.Update());
            if(userInputSystem.Dashing)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    StartCoroutine(PlayerState.StopDash());
                    return;
                }
                else return;
            }
            if (userInputSystem.ToggleBounce)
            {
                userInputSystem.BouncingAllowed = !userInputSystem.BouncingAllowed;
                if(userInputSystem.BouncingAllowed && userInputSystem.Landed)
                {
                    SetPlayerState(new Bouncing(this, userInputSystem));
                    userInputSystem.Landed = false;
                }
                userInputSystem.ToggleBounce = false;
            }
            if (userInputSystem.DoubleJumping)
            {
                SetPlayerState(new DoubleJumping(this, userInputSystem));
            }
            if (userInputSystem.Moving)
            {
                SetPlayerState(new Moving(this, userInputSystem));
            }
            if (userInputSystem.DashingConditions)
            {
                SetPlayerState(new Dashing(this, userInputSystem));
                PlayerState.Start();
                return;
            }
            if (userInputSystem.Landed)
            {
                userInputSystem.Jumping = userInputSystem.JumpingAllowed && userInputSystem.MovingUp;
                userInputSystem.Bouncing = userInputSystem.BouncingAllowed;

                if (userInputSystem.Jumping)
                {
                    SetPlayerState(new Jumping(this, userInputSystem));
                    userInputSystem.Landed = false;
                }
                else if (userInputSystem.Bouncing && !userInputSystem.BouncingDone)
                {
                    SetPlayerState(new Bouncing(this, userInputSystem));
                    userInputSystem.BouncingDone = true;
                }
            }
            */
        }
    }
    public void FixedUpdate()
    {
        /*
        if (PlayerState != null && IsGamePlaying())
        {
            StartCoroutine(PlayerState.FixedUpdate());
            if (userInputSystem.Dashing) StartCoroutine(PlayerState.Dash());
        }
        */
    }
    private static bool IsGamePlaying()
    {
        return GameController.instance.GetGameStatus() == GameController.GameStatus.Started ||
            GameController.instance.GetGameStatus() == GameController.GameStatus.Resumed;
    }
}
