using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable : PlayerState
{
    public Enable(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Enabling";
        Player.enabled = true;
    }
}
