using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dive : PlayerState
{
    public Dive(GameModeController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {
        yield break;
    }
}
