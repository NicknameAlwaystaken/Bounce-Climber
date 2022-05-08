using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable : PlayerState
{
    public Enable(PlayerController controller, Player player) : base(controller, player)
    {
    }
    public override IEnumerator Start()
    {
        Player.currentStateName = "Enabling";
        Player.enabled = true;
        yield break;
    }
}
