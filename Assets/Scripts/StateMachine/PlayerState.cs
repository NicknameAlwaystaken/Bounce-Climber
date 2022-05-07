using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController PlayerController;
    protected Player Player;

    protected PlayerState(PlayerController controller, Player player)
    {
        this.PlayerController = controller;
        this.Player = player;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Exit()
    {
        PlayerController.SetPlayerState(new Bouncing(PlayerController,Player));
        yield break;
    }
    public virtual IEnumerator Jump()
    {
        yield break;
    }
    public virtual IEnumerator Update()
    {
        yield break;
    }

}
