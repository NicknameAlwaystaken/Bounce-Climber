using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dive : PlayerState
{
    public Dive(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Diving";
    }

    public override IEnumerator Start()
    {
        yield break;
    }
}
