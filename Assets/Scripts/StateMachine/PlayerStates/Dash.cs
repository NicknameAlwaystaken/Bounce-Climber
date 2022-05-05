using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : PlayerState
{
    public Dash(GameModeController controller) : base(controller)
    {
    }
    public override IEnumerator Start()
    {
        yield break;
    }

}
