using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable : PlayerState
{
    public Enable(PlayerController controller, Player player) : base(controller, player)
    {
        UserInputSystem.currentStateName = "Enabling";
        UserInputSystem.enabled = true;
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}
