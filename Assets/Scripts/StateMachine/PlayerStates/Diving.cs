using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diving : PlayerState
{
    public Diving(PlayerController controller, Player player) : base(controller, player)
    {
    }

    public override IEnumerator Start()
    {
        yield break;
    }
    public override IEnumerator Dive()
    {
        Player.currentStateName = "Diving";
        yield break;
    }
}
