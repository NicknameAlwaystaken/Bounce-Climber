using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : PlayerState
{
    public Disable(PlayerController controller, Player player) : base(controller, player)
    {
        UserInputSystem.currentStateName = "Disabling";
        UserInputSystem.enabled = false;
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}
