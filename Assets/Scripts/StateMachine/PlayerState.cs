using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController PlayerController;
    protected Player UserInputSystem;

    protected PlayerState(PlayerController controller, Player player)
    {
        this.PlayerController = controller;
        this.UserInputSystem = player;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Exit()
    {
        PlayerController.SetPlayerState(new Bouncing(PlayerController,UserInputSystem));
        yield break;
    }
    public virtual IEnumerator Move()
    {
        yield break;
    }
    public virtual IEnumerator Jump()
    {
        yield break;
    }
    public virtual IEnumerator Bounce()
    {
        yield break;
    }
    public virtual IEnumerator DoubleJump()
    {
        yield break;
    }
    public virtual IEnumerator Dive()
    {
        yield break;
    }
    public virtual IEnumerator Dash()
    {
        yield break;
    }
    public virtual IEnumerator StopDash()
    {
        yield break;
    }
    public virtual IEnumerator Die()
    {
        yield break;
    }
    public virtual IEnumerator CurrentState()
    {
        yield break;
    }
    public virtual IEnumerator Update()
    {
        yield break;
    }
    public virtual IEnumerator FixedUpdate()
    {
        yield break;
    }

}
