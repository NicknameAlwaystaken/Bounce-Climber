using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : GameState
{
    public PauseGame(GameController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {
        yield break;
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}
