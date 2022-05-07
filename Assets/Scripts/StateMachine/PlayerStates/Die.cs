using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : PlayerState
{
    public Die(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Dying";
    }
    public override IEnumerator Start()
    {
        yield break;
    }
}