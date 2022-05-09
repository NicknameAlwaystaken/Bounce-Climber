using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameController GameController;

    protected GameState(GameController gameModeController)
    {
        this.GameController = gameModeController;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Restart()
    {
        yield break;
    }
    public virtual IEnumerator Pause()
    {
        yield break;
    }

    public virtual IEnumerator Update()
    {
        yield break;
    }
}
