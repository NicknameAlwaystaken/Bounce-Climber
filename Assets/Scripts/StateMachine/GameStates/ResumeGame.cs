using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : GameState
{
    public ResumeGame(GameController controller) : base(controller)
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
