using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : PlayerState
{
    public Die(GameModeController controller) : base(controller)
    {
    }
    public override IEnumerator Start()
    {
        yield break;
    }
}