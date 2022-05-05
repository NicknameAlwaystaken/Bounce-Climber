using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBreaks : PlayerState
{
    public NoBreaks(GameModeController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {
        yield break;
    }
}
