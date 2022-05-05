using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected GameModeController GameModeController;

    protected PlayerState(GameModeController controller)
    {
        this.GameModeController = controller;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Exit()
    {
        yield break;
    }
    public virtual IEnumerator Spawning()
    {
        yield break;
    }

    public virtual IEnumerator Dying()
    {
        yield break;
    }
    public virtual IEnumerator Diving()
    {
        yield break;
    }
    public virtual IEnumerator Bouncing()
    {
        yield break;
    }
    public virtual void Update()
    {

    }

}
