using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : PlayerState
{
    public Disable(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Disabling";
        Player.enabled = false;
    }
}
