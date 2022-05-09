using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : GameState
{
    public EndGame(GameController controller) : base(controller)
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
