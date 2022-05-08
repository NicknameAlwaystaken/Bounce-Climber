using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : PlayerState
{
    public Dashing(PlayerController controller, Player player) : base(controller, player)
    {
    }
    public override IEnumerator Start()
    {
        yield break;
    }
    public override IEnumerator Dash()
    {
        Player.currentStateName = "Dashing";
        yield break;

    }

}
