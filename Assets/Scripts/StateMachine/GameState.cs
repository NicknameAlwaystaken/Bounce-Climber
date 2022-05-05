using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameModeController GameModeController;

    protected GameState(GameModeController gameModeController)
    {
        this.GameModeController = gameModeController;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual void Update()
    {

    }
}
