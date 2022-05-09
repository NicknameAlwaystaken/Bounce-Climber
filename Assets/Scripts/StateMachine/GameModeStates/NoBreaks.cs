using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBreaks : GameState
{
    public NoBreaks(GameController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {
        yield break;
    }
}
