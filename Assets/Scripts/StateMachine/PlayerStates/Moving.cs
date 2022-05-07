using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : PlayerState
{

    public Moving(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Moving";
    }

    public override IEnumerator Start()
    {
        Player.MovingAllowed = true;
        yield break;
    }
}
