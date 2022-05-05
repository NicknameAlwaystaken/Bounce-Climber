using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : PlayerState
{

    public Moving(GameModeController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {

        GameModeController.SetPlayerState(new Bounce(GameModeController));
        yield break;
    }
}
