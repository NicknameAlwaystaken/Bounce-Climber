using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : PlayerState
{
    public Dash(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Dashing";
    }
    public override IEnumerator Start()
    {
        yield break;
    }

}
